using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Poultry_website.Helpers
{
    public static class JwtHelper
    {
        public static string GenerateToken(Guid userId, string email, IConfiguration config)
        {
            var jwtSettings = config.GetSection("JwtSettings");
            var secretKey = jwtSettings["SecretKey"];
            var issuer = jwtSettings["Issuer"];
            var audience = jwtSettings["Audience"];

            var keyBytes = Encoding.UTF8.GetBytes(secretKey);
            var key = new SymmetricSecurityKey(keyBytes);

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, userId.ToString()),

               new Claim(ClaimTypes.Email, email),

                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer,
                audience,
                claims,
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
