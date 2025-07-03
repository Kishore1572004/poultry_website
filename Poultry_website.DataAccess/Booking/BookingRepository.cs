using Poultry_website.DataAccess;
using Poultry_website.Domain.Entities;
using Poultry_website.Domain.Interfaces.Booking;
using System;
using System.Threading.Tasks;

namespace Poultry_website.DataAccess
{
    public class BookingRepository : IBookingRepository
    {
        private readonly PoultryData _context;

        public BookingRepository(PoultryData context)
        {
            _context = context;
        }

        public async Task<bool> AddChickBookingAsync(ChickBooking model)
        {
            try
            {
                await _context.ChickBookings.AddAsync(model);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> AddHatchBookingAsync(HatchBooking model)
        {
            try
            {
                await _context.HatchBookings.AddAsync(model);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> AddOrderBookingAsync(OrderBooking model)
        {
            try
            {
                await _context.OrderBookings.AddAsync(model);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
