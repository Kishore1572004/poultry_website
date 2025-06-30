using System;
using System.ComponentModel.DataAnnotations;

namespace Poultry_website.Models
{
    public class ChickBooking
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Address { get; set; }

        [Range(1, 10000)]
        public int NoOfChicks { get; set; }

        [Required]
        public DateTime BookingDate { get; set; }

        public Guid UserId { get; set; }
    }
}

