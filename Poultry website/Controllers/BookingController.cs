using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Poultry_website.Domain.Entities;
using Poultry_website.Domain.Interfaces.Booking;
using System.Security.Claims;

namespace Poultry_website.Controllers
{
    [Authorize]
    public class BookingController : Controller
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService) => _bookingService = bookingService;

        [HttpPost]
        public async Task<IActionResult> SubmitChickBooking(ChickBooking model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (ModelState.IsValid)
            {
                var success = await _bookingService.SubmitChickBookingAsync(userId, model);
                if (success)
                {
                    TempData["Success"] = "Booking submitted successfully!";
                    return RedirectToAction("Index", "Home");
                }
            }
            TempData["Error"] = "Booking failed. Please try again.";
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SubmitHatchBooking(HatchBooking model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (ModelState.IsValid)
            {
                var success = await _bookingService.SubmitHatchBookingAsync(userId, model);
                if (success)
                {
                    TempData["Success"] = "Hatchery booking successful!";
                    return RedirectToAction("Index", "Home");
                }
            }
            TempData["Error"] = "Failed to book hatchery. Please check all fields.";
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> SubmitOrderBooking(OrderBooking model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (ModelState.IsValid)
            {
                var success = await _bookingService.SubmitOrderBookingAsync(userId, model);
                if (success)
                {
                    TempData["Success"] = "Order booking successful!";
                    return RedirectToAction("Index", "Home");
                }
            }
            TempData["Error"] = "Failed to book order. Try again.";
            return RedirectToAction("Index", "Home");
        }
    }
}
