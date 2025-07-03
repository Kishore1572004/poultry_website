using System;

namespace Poultry_website.Domain.Entities
{
    public class GalleryItem
    {
        public int Id { get; set; }
        public string? Image { get; set; }
        public string? Title { get; set; }
        public string? Line1 { get; set; }
        public string? Link { get; set; }
    }
}
