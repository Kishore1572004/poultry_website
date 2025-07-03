using System.Collections.Generic;
using Poultry_website.Domain.Entities;

namespace Poultry_website.Models
{
    public class HomePageViewModel
    {
        public List<GalleryItem> GalleryItems  { get; set; }
        public ChickBooking ChickBooking { get; set; }
    }
}
