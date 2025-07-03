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
            if (Guid.TryParse(userId, out Guid parsedUserId))
            {
                model.UserId = parsedUserId;
                model.BookingDate = DateTime.Now;
                return await _bookingRepository.AddChickBookingAsync(model);
            }
            return false;
        }

        public async Task<bool> SubmitHatchBookingAsync(string userId, HatchBooking model)
        {
            if (Guid.TryParse(userId, out Guid parsedUserId))
            {
                model.UserId = parsedUserId;
                model.BookingDate = DateTime.Now;
                return await _bookingRepository.AddHatchBookingAsync(model);
            }
            return false;
        }

        public async Task<bool> SubmitOrderBookingAsync(string userId, OrderBooking model)
        {
            if (Guid.TryParse(userId, out Guid parsedUserId))
            {
                model.UserId = parsedUserId;
                model.BookingDate = DateTime.Now;
                return await _bookingRepository.AddOrderBookingAsync(model);
            }
            return false;
        }
    }
}
