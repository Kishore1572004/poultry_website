using Poultry_website.Domain.Entities;
using Poultry_website.Domain.Interfaces.Booking;
using System;
using System.Collections.Generic;
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

        // ---- Chick Booking ----

        // Adds new chick booking
        public async Task<bool> SubmitChickBookingAsync(string userId, ChickBooking model)
        {
            try
            {
                if (Guid.TryParse(userId, out Guid parsedUserId))
                {
                    model.UserId = parsedUserId;
                    model.BookingDate = DateTime.UtcNow;
                    model.CreatedAt = DateTime.UtcNow;
                    return await _bookingRepository.AddChickBookingAsync(userId, model);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in SubmitChickBookingAsync: {ex.Message}");
            }
            return false;
        }

        // Gets all chick bookings for user
        public async Task<List<ChickBooking>> GetUserChickBookingsAsync(string userId)
        {
            try
            {
                return await _bookingRepository.GetUserChickBookingsAsync(userId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetUserChickBookingsAsync: {ex.Message}");
                return new List<ChickBooking>();
            }
        }

        public async Task<ChickBooking> GetChickBookingByIdAsync(int id)
        {
            return await _bookingRepository.GetChickBookingByIdAsync(id);
        }

        // Updates booking only if within 24 hours
        public async Task<bool> UpdateChickBookingAsync(ChickBooking updated)
        {
            try
            {
                var existing = await _bookingRepository.GetChickBookingByIdAsync(updated.Id);
                if (existing == null) return false;

                if ((DateTime.UtcNow - (existing.CreatedAt ?? DateTime.UtcNow)).TotalHours > 24)
                    return false;

                existing.Name = updated.Name;
                existing.Phone = updated.Phone;
                existing.Address = updated.Address;
                existing.NoOfChicks = updated.NoOfChicks;
                existing.BookingDate = DateTime.UtcNow;

                return await _bookingRepository.UpdateChickBookingAsync(existing);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdateChickBookingAsync: {ex.Message}");
                return false;
            }
        }

        // Deletes chick booking
        public async Task<bool> DeleteChickBookingAsync(int id)
        {
            try
            {
                return await _bookingRepository.DeleteChickBookingAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in DeleteChickBookingAsync: {ex.Message}");
                return false;
            }
        }

        // Checks if booking can still be edited (within 24 hours)
        public bool IsChickBookingEditable(ChickBooking booking)
        {
            return booking.BookingDate.HasValue &&
                   (DateTime.UtcNow - booking.BookingDate.Value).TotalHours <= 24;
        }

        // ---- Hatch Booking ----

        // Adds new hatch booking
        public async Task<bool> SubmitHatchBookingAsync(string userId, HatchBooking model)
        {
            try
            {
                if (Guid.TryParse(userId, out Guid parsedUserId))
                {
                    model.UserId = parsedUserId;
                    model.BookingDate = DateTime.UtcNow;
                    model.CreatedAt = DateTime.UtcNow;
                    return await _bookingRepository.AddHatchBookingAsync(userId, model);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in SubmitHatchBookingAsync: {ex.Message}");
            }
            return false;
        }

        // Gets all hatch bookings for user
        public async Task<List<HatchBooking>> GetUserHatchBookingsAsync(string userId)
        {
            try
            {
                return await _bookingRepository.GetUserHatchBookingsAsync(userId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetUserHatchBookingsAsync: {ex.Message}");
                return new List<HatchBooking>();
            }
        }

        public async Task<HatchBooking> GetHatchBookingByIdAsync(int id)
        {
            return await _bookingRepository.GetHatchBookingByIdAsync(id);
        }

        // Updates hatch booking if under 24 hours
        public async Task<bool> UpdateHatchBookingAsync(HatchBooking updated)
        {
            try
            {
                var existing = await _bookingRepository.GetHatchBookingByIdAsync(updated.Id);
                if (existing == null) return false;

                if ((DateTime.UtcNow - (existing.CreatedAt ?? DateTime.UtcNow)).TotalHours > 24)
                    return false;

                existing.Name = updated.Name;
                existing.Address = updated.Address;
                existing.BreedName = updated.BreedName;
                existing.NumberOfEggs = updated.NumberOfEggs;
                existing.Instructions = updated.Instructions;
                existing.BookingDate = DateTime.UtcNow;

                return await _bookingRepository.UpdateHatchBookingAsync(existing);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdateHatchBookingAsync: {ex.Message}");
                return false;
            }
        }

        // Deletes hatch booking
        public async Task<bool> DeleteHatchBookingAsync(int id)
        {
            try
            {
                return await _bookingRepository.DeleteHatchBookingAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in DeleteHatchBookingAsync: {ex.Message}");
                return false;
            }
        }

        public bool IsHatchBookingEditable(HatchBooking booking)
        {
            return booking.BookingDate.HasValue &&
                   (DateTime.UtcNow - booking.BookingDate.Value).TotalHours <= 24;
        }

        // ---- Order Booking ----

        // Adds order booking
        public async Task<bool> SubmitOrderBookingAsync(string userId, OrderBooking model)
        {
            try
            {
                if (Guid.TryParse(userId, out Guid parsedUserId))
                {
                    model.UserId = parsedUserId;
                    model.BookingDate = DateTime.UtcNow;
                    model.CreatedAt = DateTime.UtcNow;
                    return await _bookingRepository.AddOrderBookingAsync(userId, model);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in SubmitOrderBookingAsync: {ex.Message}");
            }
            return false;
        }

        // Gets user’s order bookings
        public async Task<List<OrderBooking>> GetUserOrderBookingsAsync(string userId)
        {
            try
            {
                if (Guid.TryParse(userId, out Guid parsedUserId))
                {
                    return await _bookingRepository.GetAllOrderBookingsByUserIdAsync(parsedUserId);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetUserOrderBookingsAsync: {ex.Message}");
            }
            return new List<OrderBooking>();
        }

        public async Task<OrderBooking> GetOrderBookingByIdAsync(int id)
        {
            return await _bookingRepository.GetOrderBookingByIdAsync(id);
        }

        // Updates order booking if still under 24 hours
        public async Task<bool> UpdateOrderBookingAsync(OrderBooking updated)
        {
            try
            {
                var existing = await _bookingRepository.GetOrderBookingByIdAsync(updated.Id);
                if (existing == null) return false;

                if ((DateTime.UtcNow - (existing.CreatedAt ?? DateTime.UtcNow)).TotalHours > 24)
                    return false;

                existing.Name = updated.Name;
                existing.Phone = updated.Phone;
                existing.Address = updated.Address;
                existing.Category = updated.Category;
                existing.CockCount = updated.CockCount;
                existing.HenCount = updated.HenCount;
                existing.PigeonPairs = updated.PigeonPairs;
                existing.EggCount = updated.EggCount;
                existing.BookingDate = DateTime.UtcNow;

                return await _bookingRepository.UpdateOrderBookingAsync(existing);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdateOrderBookingAsync: {ex.Message}");
                return false;
            }
        }

        // Deletes order booking
        public async Task<bool> DeleteOrderBookingAsync(int id)
        {
            try
            {
                return await _bookingRepository.DeleteOrderBookingAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in DeleteOrderBookingAsync: {ex.Message}");
                return false;
            }
        }

        public bool IsOrderBookingEditable(OrderBooking booking)
        {
            return booking.BookingDate.HasValue &&
                   (DateTime.UtcNow - booking.BookingDate.Value).TotalHours <= 24;
        }
    }
}
