using Poultry_website.Domain.Entities;
using System.Collections.Generic;

namespace Poultry_website.ViewModels
{
    public class BookingModel
    {
        public List<OrderBooking> OrderBookings { get; set; } = new List<OrderBooking>();
        public List<ChickBooking> ChickBookings { get; set; } = new List<ChickBooking>();
        public List<HatchBooking> HatchBookings { get; set; } = new List<HatchBooking>();
        public string Message { get; set; } = string.Empty;
    }
}
