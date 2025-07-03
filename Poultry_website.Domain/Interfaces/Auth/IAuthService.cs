//using Poultry_website.DTO;
//using Poultry_website.Domain.Entities;
//using System.Threading.Tasks;


//namespace Poultry_website.Domain.Interfaces.Auth
//{
//    public interface IAuthService
//    {
//        Task<bool> RegisterAsync(RegisterViewModel vm);
//        Task<(bool Success, string Token, User User)> LoginAsync(LoginModel vm);
//    }
//}
using Poultry_website.Domain.Entities;
using System.Threading.Tasks;


namespace Poultry_website.Domain.Interfaces.Auth
{
    public interface IAuthService
    {
        Task<bool> RegisterAsync(string fullName,string email,string password,string confirmPassword); 
        Task<(bool Success, string Token,  string Email,string Password)> LoginAsync(string email, string password); 
    }
}

