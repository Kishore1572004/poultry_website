using Poultry_website.Domain.Entities;
using Poultry_website.Domain.Interfaces.Home;
using System.Collections.Generic;
using System.Linq;

namespace Poultry_website.DataAccess
{
    public class HomeRepository : IHomeRepository
    {
        private readonly PoultryData _context;

        public HomeRepository(PoultryData context)
        {
            _context = context;
        }

        public List<GalleryItem> GetGalleryItems()
        {
            return _context.GalleryItems.ToList();
        }

        public User GetUserByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email);
        }
    }
}
