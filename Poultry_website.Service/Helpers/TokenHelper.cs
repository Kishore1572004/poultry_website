using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Poultry_website.Helpers
{
    public static class TokenHelper
    {
        /// <summary>
        /// Validates the provided JWT token using the secret key and other settings from configuration.
        /// </summary>
        /// <param name = "token" > JWT token string</param>
        /// <param name = "config" > Application configuration for JWT settings</param>
        /// <returns>ClaimsPrincipal if token is valid; otherwise, null</returns>
        public static ClaimsPrincipal? ValidateToken(string token, IConfiguration config)
        {
            if (string.IsNullOrEmpty(token)) return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtKey = config["JwtSettings:SecretKey"];

            if (string.IsNullOrEmpty(jwtKey))
                throw new Exception("JWT key is missing in appsettings.json");

            var key = Encoding.ASCII.GetBytes(jwtKey);

            try
            {
                var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                return principal;
            }
            catch
            {
                return null; // invalid token
            }
        }

    }
}
