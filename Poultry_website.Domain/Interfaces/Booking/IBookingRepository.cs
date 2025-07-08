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

        Task<List<OrderBooking>> GetAllOrderBookingsByUserIdAsync(Guid userId);
        Task<OrderBooking> GetOrderBookingByIdAsync(int id);
        Task<bool> UpdateOrderBookingAsync(OrderBooking booking);
        Task<bool> DeleteOrderBookingAsync(int id);


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
