using System;
using System.ComponentModel.DataAnnotations;

namespace Poultry_website.Models
{
    public class HatchBooking
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime BookingDate { get; set; }

        [Required]
        public int NumberOfEggs { get; set; }

        [Required]
        public string BreedName { get; set; }

        public string Instructions { get; set; }

        [Required]
        public Guid UserId { get; set; }
    }
}
