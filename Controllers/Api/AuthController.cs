using System;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using LibraryManagementService.DTOs;
using LibraryManagementService.Models;
using Microsoft.AspNetCore.Authorization; 

namespace LibraryManagement.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]

    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _config;

        public AuthController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration config)
        {
            _userManager   = userManager;
            _signInManager = signInManager;
            _config        = config;
        }

        [HttpPost("register")]
        
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existing = await _userManager.FindByNameAsync(dto.Username);
            if (existing != null)
                return Conflict("Username already taken.");

            var user = new ApplicationUser
            {
                UserName  = dto.Username,
                Email     = dto.Email,
                FirstName = dto.FirstName,
                LastName  = dto.LastName
            };

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok(new
            {
                message  = "User registered successfully",
                user.Id,
                user.UserName,
                user.Email
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByNameAsync(dto.Username);
            if (user == null)
                return Unauthorized("Invalid username or password.");

            var signInResult = 
                await _signInManager.CheckPasswordSignInAsync(user, dto.Password, lockoutOnFailure: false);
            if (!signInResult.Succeeded)
                return Unauthorized("Invalid username or password.");

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name,           user.UserName),
                new Claim(ClaimTypes.Email,          user.Email)
            };

            var key   = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer:             _config["Jwt:Issuer"],
                audience:           _config["Jwt:Audience"],
                claims:             claims,
                expires:            DateTime.UtcNow.AddHours(2),
                signingCredentials: creds
            );

            return Ok(new 
            {
                token = new JwtSecurityTokenHandler().WriteToken(token)
            });
        }
         /// <summary>
        /// Logs the user out (for JWT, client should just discard the token).
        /// </summary>
        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            // If you ever add cookie-based auth, this clears the cookie.
            await _signInManager.SignOutAsync();

            return Ok(new { message = "Logged out successfully." });
        }
        /// <summary>
        /// Returns the current user’s details.
        /// </summary>
        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> Me()
        {
            // Pull the user ID from the token’s claims
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized();

            // Load the user from the database
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();

            // Return only the fields you want exposed
            return Ok(new
            {
                user.Id,
                user.UserName,
                user.Email,
                user.FirstName,
                user.LastName
            });
        }

        /// <summary>
        /// Changes the current user's password.
        /// </summary>
        [HttpPost("changepassword")]
        [Authorize]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // 1) Prevent same-as-old:
            if (dto.CurrentPassword == dto.NewPassword)
                return BadRequest(new { 
                    message = "New password must be different from the current password." 
                });

            // 2) Identify the user
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized();

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound();

            // 3) Attempt change
            var result = await _userManager.ChangePasswordAsync(
                user, dto.CurrentPassword, dto.NewPassword);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok(new { message = "Password changed successfully." });
        }
    }
}
