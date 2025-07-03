using Microsoft.Extensions.Configuration;
using Poultry_website.Domain;
using Poultry_website.Domain.Entities;
using Poultry_website.Helpers;
using System;
using System.Threading.Tasks;

namespace Poultry_website.Domain.Interfaces.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;

        public AuthService(IAuthRepository repo, IConfiguration config)
        {
            _repo = repo;
            _config = config;
        }

        public async Task<bool> RegisterAsync(string fullName, string email, string password, string confirmPassword)
        {
            var user = new User
            {
                FullName = fullName,
                Email = email,
                PasswordHash = PasswordHelper.HashPassword(password),
                CreatedAt = DateTime.Now
            };
            await _repo.AddUserAsync(user);
            return true;
        }

        public async Task<(bool Success, string Token, string Email, string Password)> LoginAsync(string email, string password)
        {
            var user = await _repo.GetUserByEmailAsync(email);
            if (user == null || !PasswordHelper.VerifyPassword(password, user.PasswordHash))
                return (false, null, null, null);

            var token = JwtHelper.GenerateToken(user.UserId, user.Email, _config);
            return (true, token, user.Email, password); // ⚠️ Not recommended to return password in real apps
        }

    }
}
