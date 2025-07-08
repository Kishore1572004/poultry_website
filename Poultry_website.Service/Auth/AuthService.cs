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
        private readonly IAuthRepository _repo;       // To connect with database
        private readonly IConfiguration _config;      // To read settings like secret keys

        // Constructor to set up database and config
        public AuthService(IAuthRepository repo, IConfiguration config)
        {
            _repo = repo;
            _config = config;
        }

        // To register new user
        public async Task<bool> RegisterAsync(string fullName, string email, string password, string confirmPassword)
        {
            try
            {
                // Create new user with name, email, and password 
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
                // Show error in console if something fails
                Console.WriteLine($"Error in RegisterAsync: {ex.Message}");
                return false;
            }
        }

        // To log in a user and return token if correct
        public async Task<(bool Success, string Token, string Email, string Password, Guid UserId, string FullName)> LoginAsync(string email, string password)
        {
            try
            {
                // Get user from database by email
                var user = await _repo.GetUserByEmailAsync(email);

                // If user not found or password does not match, return failure
                if (user == null || !PasswordHelper.VerifyPassword(password, user.PasswordHash))
                    return (false, null!, null!, null!, Guid.Empty, null!);

                // Create token using user ID and email
                var token = JwtHelper.GenerateToken(user.UserId, user.Email, _config);

                // Return success and user info
                return (true, token, user.Email, password, user.UserId, user.FullName ?? "");
            }
            catch (Exception ex)
            {
                // Show error in console if something fails
                Console.WriteLine($"Error in LoginAsync: {ex.Message}");
                return (false, null!, null!, null!, Guid.Empty, null!);
            }
        }
    }
}
