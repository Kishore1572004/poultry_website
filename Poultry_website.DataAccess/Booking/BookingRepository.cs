using Microsoft.EntityFrameworkCore;
using Poultry_website.DataAccess;
using Poultry_website.Domain.Entities;
using Poultry_website.Domain.Interfaces.Booking;
using System;
using System.Collections.Generic;
using System.Linq;
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

        // ----------------- ADD METHODS -----------------

        public async Task<bool> AddChickBookingAsync(string userId, ChickBooking model)
        {
            try
            {
                model.UserId = Guid.Parse(userId);
                await _context.ChickBookings.AddAsync(model);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding ChickBooking: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> AddHatchBookingAsync(string userId, HatchBooking model)
        {
            try
            {
                model.UserId = Guid.Parse(userId);
                await _context.HatchBookings.AddAsync(model);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding HatchBooking: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> AddOrderBookingAsync(string userId, OrderBooking model)
        {
            try
            {
                model.UserId = Guid.Parse(userId);
                await _context.OrderBookings.AddAsync(model);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding OrderBooking: {ex.Message}");
                return false;
            }
        }

        // ----------------- UPDATE METHODS -----------------

        public async Task<bool> UpdateOrderBookingAsync(OrderBooking booking)
        {
            try
            {
                _context.OrderBookings.Update(booking);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating OrderBooking: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateChickBookingAsync(ChickBooking booking)
        {
            try
            {
                _context.ChickBookings.Update(booking);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating ChickBooking: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateHatchBookingAsync(HatchBooking booking)
        {
            try
            {
                _context.HatchBookings.Update(booking);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating HatchBooking: {ex.Message}");
                return false;
            }
        }

        // ----------------- DELETE METHODS -----------------

        public async Task<bool> DeleteOrderBookingAsync(int id)
        {
            try
            {
                var booking = await _context.OrderBookings.FindAsync(id);
                if (booking == null || !booking.CreatedAt.HasValue ||
                    (DateTime.UtcNow - booking.CreatedAt.Value).TotalHours > 24)
                    return false;

                _context.OrderBookings.Remove(booking);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting OrderBooking: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteChickBookingAsync(int id)
        {
            try
            {
                var booking = await _context.ChickBookings.FindAsync(id);
                if (booking == null)
                    return false;

                _context.ChickBookings.Remove(booking);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting ChickBooking: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteHatchBookingAsync(int id)
        {
            try
            {
                var booking = await _context.HatchBookings.FindAsync(id);
                if (booking == null)
                    return false;

                _context.HatchBookings.Remove(booking);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting HatchBooking: {ex.Message}");
                return false;
            }
        }

        // ----------------- GET SINGLE BY ID -----------------

        public async Task<OrderBooking> GetOrderBookingByIdAsync(int id)
        {
            return await _context.OrderBookings.FindAsync(id);
        }

        public async Task<ChickBooking> GetChickBookingByIdAsync(int id)
        {
            return await _context.ChickBookings.FindAsync(id);
        }

        public async Task<HatchBooking> GetHatchBookingByIdAsync(int id)
        {
            return await _context.HatchBookings.FindAsync(id);
        }

        // ----------------- GET ALL BY USER -----------------

        public async Task<List<OrderBooking>> GetAllOrderBookingsByUserIdAsync(Guid userId)
        {
            try
            {
                return await _context.OrderBookings
                    .Where(o => o.UserId == userId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching OrderBookings: {ex.Message}");
                return new List<OrderBooking>();
            }
        }

        public async Task<List<ChickBooking>> GetUserChickBookingsAsync(string userId)
        {
            if (!Guid.TryParse(userId, out var guid))
                return new List<ChickBooking>();

            return await _context.ChickBookings
                .Where(b => b.UserId == guid)
                .ToListAsync();
        }

        public async Task<List<HatchBooking>> GetUserHatchBookingsAsync(string userId)
        {
            if (!Guid.TryParse(userId, out var guid))
                return new List<HatchBooking>();

            return await _context.HatchBookings
                .Where(b => b.UserId == guid)
                .ToListAsync();
        }

        // ----------------- EDITABLE CHECKERS (optional usage) -----------------

        public bool IsChickBookingEditable(ChickBooking booking) =>
            booking.CreatedAt.HasValue &&
            (DateTime.UtcNow - booking.CreatedAt.Value).TotalHours <= 24;

        public bool IsHatchBookingEditable(HatchBooking booking) =>
            booking.CreatedAt.HasValue &&
            (DateTime.UtcNow - booking.CreatedAt.Value).TotalHours <= 24;

        public bool IsOrderBookingEditable(OrderBooking booking) =>
            booking.CreatedAt.HasValue &&
            (DateTime.UtcNow - booking.CreatedAt.Value).TotalHours <= 24;
    }
}
