using Microsoft.AspNetCore.Mvc;
using Poultry_website.Data;
using Poultry_website.Models;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Poultry_website.Controllers
{

    [Authorize]
    public class BookingController : Controller
    {
        private readonly PoultryData _context;

        public BookingController(PoultryData context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> SubmitChickBooking(ChickBooking model)
        {
            if (ModelState.IsValid)
            {
                // 🟩 Get logged-in user's ID
                var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (Guid.TryParse(userIdString, out Guid userId))
                {
                    model.UserId = userId;
                    model.BookingDate = DateTime.Now;

                    _context.ChickBookings.Add(model);
                    await _context.SaveChangesAsync();

                    TempData["Success"] = "Booking submitted successfully!";
                    return RedirectToAction("index", "home");  // Or your target page
                }
                else
                {
                    ModelState.AddModelError("", "User not authenticated.");
                }
            }

            return View(model);
        }



        //  HATCH BOOKING
        [HttpPost]
        public IActionResult SubmitOrderBooking(OrderBooking model)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (Guid.TryParse(userId, out Guid parsedUserId))
                {
                    model.UserId = parsedUserId;
                    model.BookingDate = DateTime.Now;

                    _context.OrderBookings.Add(model);
                    _context.SaveChanges();

                    TempData["Success"] = "Order booking successful!";
                    return RedirectToAction("Index", "Home");
                }
            }

            TempData["Error"] = "Failed to book order. Try again.";
            return RedirectToAction("Index", "Home");
        }


        [HttpPost]
        public IActionResult SubmitHatchBooking(HatchBooking model)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (Guid.TryParse(userId, out Guid parsedUserId))
                {
                    model.UserId = parsedUserId;
                    model.BookingDate = DateTime.Now;

                    _context.HatchBookings.Add(model);
                    _context.SaveChanges();

                    TempData["Success"] = "Hatchery booking successful!";
                    return RedirectToAction("Index", "Home");
                }
            }

            TempData["Error"] = "Failed to book hatchery. Please check all fields.";
            return RedirectToAction("Index", "Home");
        }

    }

}