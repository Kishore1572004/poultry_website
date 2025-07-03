using Microsoft.EntityFrameworkCore;
using Poultry_website.DataAccess;
using Poultry_website.Domain;
using Poultry_website.Domain.Entities;
using System.Threading.Tasks;


namespace Poultry_website.Domain.Interfaces.Auth

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
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task AddUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }
    }
}
