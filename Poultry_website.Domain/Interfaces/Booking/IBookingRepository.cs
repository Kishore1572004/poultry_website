using Poultry_website.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Poultry_website.Domain.Interfaces.Booking
{
    public interface IBookingRepository
    {
        Task<bool> AddChickBookingAsync(String UserId, ChickBooking model);
        Task<bool> AddHatchBookingAsync(String UserId, HatchBooking model);
        Task<bool> AddOrderBookingAsync(String UserId, OrderBooking model);
    }
}
