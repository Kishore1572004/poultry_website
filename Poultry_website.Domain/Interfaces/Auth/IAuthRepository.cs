using Poultry_website.Domain.Entities;
using System.Threading.Tasks;

namespace Poultry_website.Domain.Interfaces.Auth
{
    public interface IAuthRepository
    {
        Task<User> GetUserByEmailAsync(string email);
        Task AddUserAsync(User user);
    }
}
