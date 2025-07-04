using Poultry_website.Domain.Entities;
using Poultry_website.Domain.Interfaces.Home;
using System;
using System.Collections.Generic;

namespace Poultry_website.Service
{
    public class HomeService : IHomeService
    {
        private readonly IHomeRepository _repository;

        public HomeService(IHomeRepository repository)
        {
            _repository = repository;
        }

        public List<GalleryItem> GetGalleryItems()
        {
            try
            {
                return _repository.GetGalleryItems();
            }
            catch (Exception ex)
            {
                // Log the error (optional): Log.Error("Failed to get gallery items", ex);
                Console.WriteLine($"Error fetching gallery items: {ex.Message}");
                return new List<GalleryItem>(); // return empty list on failure
            }
        }

        public User GetUserByEmail(string email)
        {
            try
            {
                return _repository.GetUserByEmail(email);
            }
            catch (Exception ex)
            {
                // Log.Error($"Error fetching user with email {email}", ex);
                Console.WriteLine($"Error fetching user by email: {ex.Message}");
                return null;
            }
        }
    }
}
