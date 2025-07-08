using Poultry_website.Domain.Entities;
using Poultry_website.Domain.Interfaces.Home;
using System;
using System.Collections.Generic;

namespace Poultry_website.Service
{
    public class HomeService : IHomeService
    {
        // Reference to the repository interface
        private readonly IHomeRepository _repository;

        // Constructor to inject the repository into the service
        public HomeService(IHomeRepository repository)
        {
            _repository = repository;
        }

        //  method returns a list of gallery items from the repository
        public List<GalleryItem> GetGalleryItems()
        {
            try
            {
                return _repository.GetGalleryItems();
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Error fetching gallery items: {ex.Message}");
                return new List<GalleryItem>();
            }
        }

        //  gets a user by their email from the repository
        public User GetUserByEmail(string email)
        {
            try
            {
                return _repository.GetUserByEmail(email);
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Error fetching user by email: {ex.Message}");
                return null;
            }
        }
    }
}
