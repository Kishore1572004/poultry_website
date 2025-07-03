using Microsoft.EntityFrameworkCore;
using Poultry_website.Domain.Entities;

namespace Poultry_website.DataAccess
{
    public class PoultryData : DbContext
    {
        public PoultryData(DbContextOptions<PoultryData> options) : base(options) { }

        public DbSet<GalleryItem> GalleryItems { get; set; }
        public DbSet<ChickBooking> ChickBookings { get; set; }
        public DbSet<OrderBooking> OrderBookings { get; set; }
        public DbSet<HatchBooking> HatchBookings { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // ChickBooking Config
            modelBuilder.Entity<ChickBooking>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Phone).IsRequired();
                entity.Property(e => e.Address).IsRequired();
                entity.Property(e => e.NoOfChicks).HasDefaultValue(1);
                entity.Property(e => e.BookingDate).IsRequired();
                entity.Property(e => e.UserId).IsRequired();
            });

            // HatchBooking Config
            modelBuilder.Entity<HatchBooking>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Address).IsRequired();
                entity.Property(e => e.BookingDate).IsRequired();
                entity.Property(e => e.NumberOfEggs).IsRequired();
                entity.Property(e => e.BreedName).IsRequired();
                entity.Property(e => e.UserId).IsRequired();
            });

            // OrderBooking Config
            modelBuilder.Entity<OrderBooking>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Phone).IsRequired();
                entity.Property(e => e.Address).IsRequired();
                entity.Property(e => e.Category).IsRequired();
                entity.Property(e => e.BookingDate).IsRequired();
                entity.Property(e => e.UserId).IsRequired();
                // Optional counts (nullable)
                entity.Property(e => e.CockCount);
                entity.Property(e => e.HenCount);
                entity.Property(e => e.PigeonPairs);
                entity.Property(e => e.EggCount);
            });

            // GalleryItem Config
            modelBuilder.Entity<GalleryItem>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Image).IsRequired();
                entity.Property(e => e.Title).IsRequired();
                entity.Property(e => e.Line1);
                entity.Property(e => e.Link);
            });

            // User Config
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId);
                entity.Property(e => e.FullName).IsRequired();
                entity.Property(e => e.Email).IsRequired();
                entity.Property(e => e.PasswordHash).IsRequired();
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()");
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
