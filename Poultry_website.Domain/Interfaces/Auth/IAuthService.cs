using System;
using System.Threading.Tasks;

namespace Poultry_website.Domain.Interfaces.Auth
{
    public interface IAuthService
    {
        Task<bool> RegisterAsync(string fullName, string email, string password, string confirmPassword);
        Task<(bool Success, string Token, string Email, string Password, Guid UserId, string FullName)> LoginAsync(string email, string password);
    }
}
