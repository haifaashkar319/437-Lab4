// Infrastructure/Services/JwtTokenService.cs

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;            // from System.IdentityModel.Tokens.Jwt
using System.Security.Claims;
using System.Text;
using Core.Domain.Settings;
using Infrastructure.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;              // from System.IdentityModel.Tokens.Jwt

namespace Infrastructure.Services
{
    public class JwtTokenService
    {
        private readonly JwtSettings _jwt;

        public JwtTokenService(IOptions<JwtSettings> jwt)
        {
            _jwt = jwt.Value;
        }

        public string GenerateToken(ApplicationUser user, IList<string> roles)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id),
                new(ClaimTypes.Name,           user.UserName!)
            };

            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role));

            var key   = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer:            _jwt.Issuer,
                audience:          _jwt.Audience,
                claims:            claims,
                expires:           DateTime.UtcNow.AddMinutes(_jwt.ExpiryMinutes),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
