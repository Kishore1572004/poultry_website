using System;

namespace Poultry_website.Domain.Entities
{
    public class HatchBooking
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public DateTime BookingDate { get; set; }
        public int NumberOfEggs { get; set; }
        public string? BreedName      { get; set; }
        public string? Instructions { get; set; }
        public Guid UserId { get; set; }
    }
}
