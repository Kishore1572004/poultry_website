using Poultry_website.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Poultry_website.Domain.Interfaces.Booking
{
    public interface IBookingRepository
    {
        Task<bool> AddChickBookingAsync(ChickBooking model);
        Task<bool> AddHatchBookingAsync(HatchBooking model);
        Task<bool> AddOrderBookingAsync(OrderBooking model);
    }
}
