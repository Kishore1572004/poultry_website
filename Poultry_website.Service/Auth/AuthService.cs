using Microsoft.Extensions.Configuration;
using Poultry_website.Domain.Entities;
using Poultry_website.Domain.Interfaces.Auth;
using Poultry_website.Helpers;
using System;
using System.Threading.Tasks;

namespace Poultry_website.Service.Auth
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
            try
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
            catch (Exception ex)
            {
                Console.WriteLine($"Error in RegisterAsync: {ex.Message}");
                return false;
            }
        }

        public async Task<(bool Success, string Token, string Email, string Password, Guid UserId, string FullName)> LoginAsync(string email, string password)
        {
            try
            {
                var user = await _repo.GetUserByEmailAsync(email);

                if (user == null || !PasswordHelper.VerifyPassword(password, user.PasswordHash))
                    return (false, null!, null!, null!, Guid.Empty, null!);

                var token = JwtHelper.GenerateToken(user.UserId, user.Email, _config);
                return (true, token, user.Email, password, user.UserId, user.FullName ?? "");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in LoginAsync: {ex.Message}");
                return (false, null!, null!, null!, Guid.Empty, null!);
            }
        }
    }
}
