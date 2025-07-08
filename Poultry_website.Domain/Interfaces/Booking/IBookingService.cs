using Poultry_website.Domain.Entities;
using System.Threading.Tasks;

namespace Poultry_website.Domain.Interfaces.Booking
{
    public interface IBookingService
    {
        Task<bool> SubmitChickBookingAsync(string userId, ChickBooking model);
        Task<bool> SubmitHatchBookingAsync(string userId, HatchBooking model);
        Task<bool> SubmitOrderBookingAsync(string userId, OrderBooking model);

        Task<OrderBooking> GetOrderBookingByIdAsync(int id);
        Task<List<OrderBooking>> GetUserOrderBookingsAsync(string userId);
        Task<bool> UpdateOrderBookingAsync(OrderBooking booking);
        Task<bool> DeleteOrderBookingAsync(int id);
        bool IsOrderBookingEditable(OrderBooking booking);


        // Chick
        Task<List<ChickBooking>> GetUserChickBookingsAsync(string userId);
        Task<ChickBooking> GetChickBookingByIdAsync(int id);
        bool IsChickBookingEditable(ChickBooking booking);
        Task<bool> UpdateChickBookingAsync(ChickBooking booking);
        Task<bool> DeleteChickBookingAsync(int id);

        // Hatch
        Task<List<HatchBooking>> GetUserHatchBookingsAsync(string userId);
        Task<HatchBooking> GetHatchBookingByIdAsync(int id);
        bool IsHatchBookingEditable(HatchBooking booking);
        Task<bool> UpdateHatchBookingAsync(HatchBooking booking);
        Task<bool> DeleteHatchBookingAsync(int id);



    }
}