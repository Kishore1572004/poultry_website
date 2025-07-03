using Poultry_website.Domain.Entities;
using System.Collections.Generic;

namespace Poultry_website.Domain.Interfaces.Home
{
    public interface IHomeRepository
    {
        List<GalleryItem> GetGalleryItems();
        User GetUserByEmail(string email);
    }
}
