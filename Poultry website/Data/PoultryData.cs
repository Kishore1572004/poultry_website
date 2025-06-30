using Microsoft.EntityFrameworkCore;
using Poultry_website.Models;

namespace Poultry_website.Data
{
    public class PoultryData : DbContext
    {
        public PoultryData(DbContextOptions<PoultryData> options) : base(options) { 
        }

        public DbSet<GalleryItem> GalleryItems { get; set; }
        public DbSet<ChickBooking> ChickBookings { get; set; }
        public DbSet<OrderBooking> OrderBookings { get; set; }
        public DbSet<HatchBooking> HatchBookings { get; set; }
        public DbSet<User> Users { get; set; }
      



    }
}
