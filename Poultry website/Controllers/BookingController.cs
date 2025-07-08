using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Poultry_website.Domain.Entities;
using Poultry_website.Domain.Interfaces.Booking;
using Poultry_website.ViewModels;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Poultry_website.Controllers
{
    [Authorize] 
    // All actions need user to be logged in
    public class BookingController : Controller
    {
        private readonly IBookingService _bookingService;

        // Constructor - injects service
        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        // ================== SUBMIT BOOKINGS ==================

        //  POST: Submit Chick Booking
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitChickBooking(ChickBooking model)
        {
            try
            {
                // Get logged-in user's ID
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (userId == null)
                {
                    TempData["Error"] = "Login required!";
                    return RedirectToAction("Login", "Auth");
                }

                //  Check model validity
                if (!ModelState.IsValid)
                {
                    TempData["Error"] = "Invalid chick booking data.";
                    return View(model);
                }

                //  Save booking
                var success = await _bookingService.SubmitChickBookingAsync(userId, model);
                TempData[success ? "Success" : "Error"] = success ? "Chick booking successful!" : "Failed to submit Chick booking.";
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Chick booking error: {ex.Message}";
                return RedirectToAction("Index", "Home");
            }
        }

        //  POST: Submit Hatch Booking
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitHatchBooking(HatchBooking model)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (userId == null)
                {
                    TempData["Error"] = "Login required!";
                    return RedirectToAction("Login", "Auth");
                }

                if (!ModelState.IsValid)
                {
                    TempData["Error"] = "Invalid hatch booking data.";
                    return RedirectToAction("Index", "Home");
                }

                var success = await _bookingService.SubmitHatchBookingAsync(userId, model);
                TempData[success ? "Success" : "Error"] = success ? "Hatch booking successful!" : "Failed to submit Hatch booking.";
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Hatch booking error: {ex.Message}";
                return RedirectToAction("Index", "Home");
            }
        }

        //  POST: Submit Order Booking
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitOrderBooking(OrderBooking model)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (userId == null)
                {
                    TempData["Error"] = "Login required!";
                    return RedirectToAction("Login", "Auth");
                }

                if (!ModelState.IsValid)
                {
                    TempData["Error"] = "Invalid order booking data.";
                    return RedirectToAction("Index", "Home");
                }

                var success = await _bookingService.SubmitOrderBookingAsync(userId, model);
                TempData[success ? "Success" : "Error"] = success ? "Order booking successful!" : "Failed to submit Order booking.";
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Order booking error: {ex.Message}";
                return RedirectToAction("Index", "Home");
            }
        }

        // ================== VIEW BOOKINGS ==================

        // GET: View all bookings of logged-in user
        [HttpGet]
        public async Task<IActionResult> MyOrders()
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(userId))
                {
                    TempData["Error"] = "⚠️ Login required!";
                    return RedirectToAction("Login", "Auth");
                }

                //  Get user's all bookings
                var model = new BookingModel
                {
                    OrderBookings = await _bookingService.GetUserOrderBookingsAsync(userId),
                    ChickBookings = await _bookingService.GetUserChickBookingsAsync(userId),
                    HatchBookings = await _bookingService.GetUserHatchBookingsAsync(userId),
                    Message = TempData["Message"]?.ToString()
                };

                return View("MyOrders", model); //  Render booking view
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An unexpected error occurred.";
                return RedirectToAction("Login", "Auth");
            }
        }

        //  UPDATE BOOKINGS 

        //  POST: Update Chick Booking (called via JS/AJAX)
        [HttpPost]
        public async Task<IActionResult> UpdateChickBooking([FromBody] ChickBooking model)
        {
            try
            {
                var result = await _bookingService.UpdateChickBookingAsync(model);
                return result ? Ok() : StatusCode(500, "Update failed.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error updating Chick Booking: {ex.Message}");
            }
        }

        //  POST: Update Order Booking (AJAX)
        [HttpPost]
        public async Task<IActionResult> UpdateOrderBooking([FromBody] OrderBooking model)
        {
            try
            {
                if (model == null || model.Id <= 0)
                    return BadRequest("Invalid booking data.");

                var updated = await _bookingService.UpdateOrderBookingAsync(model);
                return updated ? Ok() : StatusCode(500, "Update failed.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error updating Order Booking: {ex.Message}");
            }
        }

        //  POST: Update Hatch Booking (AJAX)
        [HttpPost]
        public async Task<IActionResult> UpdateHatchBooking([FromBody] HatchBooking model)
        {
            try
            {
                if (model == null || model.Id <= 0)
                    return BadRequest("Invalid booking data.");

                var updated = await _bookingService.UpdateHatchBookingAsync(model);
                return updated ? Ok() : StatusCode(500, "Update failed.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("=======>" + ex);
                Console.WriteLine("Inner Exception: " + ex.InnerException?.Message);
                return StatusCode(500, $"Error updating Hatch Booking: {ex.Message}");
            }
        }

        //  DELETE BOOKINGS 

        //  POST: Delete Chick Booking
        [HttpPost]
        public async Task<IActionResult> DeleteChickBooking(int id)
        {
            try
            {
                var success = await _bookingService.DeleteChickBookingAsync(id);
                TempData[success ? "Success" : "Error"] = success ? "Chick booking deleted." : "Failed to delete Chick booking.";
                return RedirectToAction("MyOrders");
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error deleting Chick Booking: {ex.Message}";
                return RedirectToAction("MyOrders");
            }
        }

        // POST: Delete Order Booking
        [HttpPost]
        public async Task<IActionResult> DeleteOrderBooking(int id)
        {
            try
            {
                var success = await _bookingService.DeleteOrderBookingAsync(id);
                TempData[success ? "Success" : "Error"] = success ? "Order booking deleted." : "Failed to delete Order booking.";
                return RedirectToAction("MyOrders");
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error deleting Order Booking: {ex.Message}";
                return RedirectToAction("MyOrders");
            }
        }

        //  POST: Delete Hatch Booking
        [HttpPost]
        public async Task<IActionResult> DeleteHatchBooking(int id)
        {
            try
            {
                var success = await _bookingService.DeleteHatchBookingAsync(id);
                TempData[success ? "Success" : "Error"] = success ? "Hatch booking deleted." : "Failed to delete Hatch booking.";
                return RedirectToAction("MyOrders");
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error deleting Hatch Booking: {ex.Message}";
                return RedirectToAction("MyOrders");
            }
        }
    }
}
