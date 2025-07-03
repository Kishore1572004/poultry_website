using Poultry_website.Domain.Entities;
using Poultry_website.Domain.Interfaces.Home;
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
            return _repository.GetGalleryItems();
        }

        public User GetUserByEmail(string email)
        {
            return _repository.GetUserByEmail(email);
        }
    }
}
