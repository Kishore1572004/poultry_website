
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using Poultry_website.Data;
using Poultry_website.Helpers;
using Poultry_website.Models;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;

namespace Poultry_website.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<HomeController> _logger;
        private readonly PoultryData _context;
        private readonly IConfiguration _config;

        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public HomeController(
            ILogger<HomeController> logger,
            IWebHostEnvironment env,
            PoultryData context,
            IConfiguration config)
        {
            _logger = logger;
            _env = env;
            _context = context;
            _config = config;
        }

        // ✅ Homepage
        public IActionResult Index()
        {
            var model = new HomePageViewModel
            {
                GalleryItems = _context.GalleryItems.ToList(),
                ChickBooking = new ChickBooking()
            };
            return View(model);
        }

        // ✅ View Routes
        [Route("/vaccine")]
        public IActionResult Vaccine() => View();

        [Route("/vaccineplanner")]
        public IActionResult VaccinePlanner() => View();

        [Route("/aseel")]
        public IActionResult Aseel() => View();

        [Route("/hatchery")]
        public IActionResult Hatchery() => View();

        [Route("/racinghomer")]
        public IActionResult RacingHomer() => View();

        [Route("/egg")]
        public IActionResult Egg() => View();

        // ✅ Logout user (clears session and token)
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            Response.Cookies.Delete("Token");
            return RedirectToAction("Index");
        }

        // ✅ Authenticated Dashboard
        [HttpGet]
        public IActionResult Dashboard()
        {
            var token = Request.Cookies["Token"];
            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Auth");

            var principal = TokenHelper.ValidateToken(token, _config);
            if (principal == null)
                return RedirectToAction("Login", "Auth");

            var email = principal.FindFirst(ClaimTypes.Email)?.Value;
            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            if (user == null)
                return RedirectToAction("Login", "Auth");

            return View("Dashboard", user);
        }

        // ✅ Default error page
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
    }
}
