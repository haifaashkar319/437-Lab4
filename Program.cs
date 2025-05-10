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
using LibraryManagementService.Data;
using LibraryManagementService.Models;
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
builder.Services.AddDbContext<LibraryContext>(opts =>
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

// Seed
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<LibraryContext>();
    db.Database.EnsureCreated();
}

// Middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

// Routes: default to Auth/Login
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Login}/{id?}");

app.Run();
