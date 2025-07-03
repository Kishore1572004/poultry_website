using Poultry_website.Domain.Entities;
using System.Threading.Tasks;

namespace Poultry_website.Domain.Interfaces.Booking
{
    public interface IBookingService
    {
        Task<bool> SubmitChickBookingAsync(string userId, ChickBooking model);
        Task<bool> SubmitHatchBookingAsync(string userId, HatchBooking model);
        Task<bool> SubmitOrderBookingAsync(string userId, OrderBooking model);
    }
}
