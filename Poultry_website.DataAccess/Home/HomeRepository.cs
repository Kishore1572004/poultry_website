using Poultry_website.Domain.Entities;
using Poultry_website.Domain.Interfaces.Home;
using System.Collections.Generic;
using System.Linq;
using System;

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
            try
            {
                return _context.GalleryItems.ToList();
            }
            catch (Exception ex)
            {
                // Log the error or handle as needed
                Console.WriteLine($"Error in GetGalleryItems: {ex.Message}");
                return new List<GalleryItem>();
            }
        }

        public User GetUserByEmail(string email)
        {
            try
            {
                return _context.Users.FirstOrDefault(u => u.Email == email);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetUserByEmail: {ex.Message}");
                return null;
            }
        }
    }
}
