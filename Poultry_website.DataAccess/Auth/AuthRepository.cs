using Microsoft.EntityFrameworkCore;
using Poultry_website.DataAccess;
using Poultry_website.Domain.Entities;
using Poultry_website.Domain.Interfaces.Auth;
using System;
using System.Threading.Tasks;

namespace Poultry_website.DataAccess.Auth
{
    public class AuthRepository : IAuthRepository
    {
        private readonly PoultryData _context;

        public AuthRepository(PoultryData context)
        {
            _context = context;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            try
            {
                return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            }
            catch (Exception ex)
            {
                // Log the error (you can inject ILogger if needed)
                Console.WriteLine($"Error in GetUserByEmailAsync: {ex.Message}");
                return null;
            }
        }

        public async Task AddUserAsync(User user)
        {
            try
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the error
                Console.WriteLine($"Error in AddUserAsync: {ex.Message}");
                throw; // rethrow so caller can handle if needed
            }
        }
    }
}
