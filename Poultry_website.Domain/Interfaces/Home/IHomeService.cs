using Poultry_website.Domain.Entities;
using System.Collections.Generic;

namespace Poultry_website.Domain.Interfaces.Home
{
    public interface IHomeService
    {
        List<GalleryItem> GetGalleryItems();
        Entities.User GetUserByEmail(string email);

    }
}
