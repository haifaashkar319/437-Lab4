using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using FluentValidation.AspNetCore;
using MediatR;
using Core.Domain.Settings;
using Infrastructure.Identity;
using Infrastructure.Persistence;
using Core.Domain.Interfaces;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Application.Books.Validators;
using Application.Books.Handlers;
using Application.Mapping;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// 1) MediatR + FluentValidation + AutoMapper
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssemblyContaining<CreateBookHandler>());
builder.Services.AddFluentValidationAutoValidation()
                .AddValidatorsFromAssemblyContaining<CreateBookValidator>();
builder.Services.AddAutoMapper(
    typeof(Application.Mapping.AuthorProfile).Assembly,
    typeof(Application.Mapping.BookProfile).Assembly,
    typeof(Application.Mapping.BorrowerProfile).Assembly,
    typeof(Application.Mapping.LoanProfile).Assembly,
    typeof(WebMappingProfile).Assembly
);

// 2) MVC + JSON
builder.Services.AddControllersWithViews()
    .AddJsonOptions(opts =>
    {
        opts.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        opts.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });

// 3) EF Core DbContexts
builder.Services.AddDbContext<CleanLibraryContext>(opts =>
    opts.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddIdentity<Infrastructure.Identity.ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<CleanLibraryContext>()
    .AddDefaultTokenProviders();


// 5) JWT Settings + Service
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));
builder.Services.AddScoped<JwtTokenService>();

// 6) Authentication Schemes (JWT for API)
builder.Services.AddAuthentication(opts =>
{
    opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opts.DefaultChallengeScheme    = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(opts =>
{
    var jwt = builder.Configuration.GetSection("Jwt").Get<JwtSettings>();
    opts.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer           = true,
        ValidateAudience         = true,
        ValidateLifetime         = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer   = jwt.Issuer,
        ValidAudience = jwt.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwt.Key))
    };
});
builder.Services.AddAuthorization();

// 7) Repositories
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IBorrowerRepository, BorrowerRepository>();
builder.Services.AddScoped<ILoanRepository, LoanRepository>();

var app = builder.Build();
//— 1) seed roles --------------------------------------
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var rolesToSeed = new[] { "Admin" /*, "User", "Manager", etc.*/ };

    foreach (var roleName in rolesToSeed)
    {
        // .GetAwaiter().GetResult() is just for a quick sync‐block;
        // in production you’d want a truly async seed.
        if (!roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
        {
            roleManager.CreateAsync(new IdentityRole(roleName))
                       .GetAwaiter()
                       .GetResult();
        }
    }
}
//— end seeding -----------------------------------------

// seed your app‐specific data (if any)
using (var scope = app.Services.CreateScope())
{
    var ctx = scope.ServiceProvider.GetRequiredService<CleanLibraryContext>();
    ctx.Database.EnsureCreated();
}

// … the rest of your middleware pipeline …
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Login}/{id?}");

app.Run();
