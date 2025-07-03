using System;

namespace Poultry_website.Domain.Entities
{
    public class ChickBooking
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public int NoOfChicks { get; set; }
        public DateTime BookingDate { get; set; }
        public Guid UserId { get; set; }
    }
}
