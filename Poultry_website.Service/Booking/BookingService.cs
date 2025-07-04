using Poultry_website.Domain.Entities;
using Poultry_website.Domain.Interfaces.Booking;
using System;
using System.Threading.Tasks;

namespace Poultry_website.Service
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;

        public BookingService(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<bool> SubmitChickBookingAsync(string userId, ChickBooking model)
        {
            try
            {
                if (Guid.TryParse(userId, out Guid parsedUserId))
                {
                    model.UserId = parsedUserId;
                    model.BookingDate = DateTime.Now;
                    return await _bookingRepository.AddChickBookingAsync(userId, model);
                }
            }
            catch (Exception ex)
            {
                // Log the exception (e.g., using a logger)
                Console.WriteLine($"Error in SubmitChickBookingAsync: {ex.Message}");
            }
            return false;
        }

        public async Task<bool> SubmitHatchBookingAsync(string userId, HatchBooking model)
        {
            try
            {
                if (Guid.TryParse(userId, out Guid parsedUserId))
                {
                    model.UserId = parsedUserId;
                    model.BookingDate = DateTime.Now;
                    return await _bookingRepository.AddHatchBookingAsync(userId, model);
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error in SubmitHatchBookingAsync: {ex.Message}");
            }
            return false;
        }

        public async Task<bool> SubmitOrderBookingAsync(string userId, OrderBooking model)
        {
            try
            {
                if (Guid.TryParse(userId, out Guid parsedUserId))
                {
                    model.UserId = parsedUserId;
                    model.BookingDate = DateTime.Now;
                    return await _bookingRepository.AddOrderBookingAsync(userId, model);
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error in SubmitOrderBookingAsync: {ex.Message}");
            }
            return false;
        }
    }
}
