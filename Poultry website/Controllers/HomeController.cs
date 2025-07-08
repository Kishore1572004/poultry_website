
using Microsoft.AspNetCore.Mvc;
using Poultry_website.Domain.Entities;
using Poultry_website.Domain.Interfaces.Home;
using Poultry_website.Helpers;
using Poultry_website.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace Poultry_website.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _env; // To access hosting environment info
        private readonly ILogger<HomeController> _logger; // For logging errors
        private readonly IHomeService _homeService; // Service for home-related data
        private readonly IConfiguration _config; // Access to appsettings.json values

        // Constructor to inject all dependencies
        public HomeController(
            ILogger<HomeController> logger,
            IWebHostEnvironment env,
            IHomeService homeService,
            IConfiguration config)
        {
            _logger = logger;
            _env = env;
            _homeService = homeService;
            _config = config;
        }

        // Home page of the website
        public IActionResult Index()
        {
            try
            {
                // Create a view model with gallery items and an empty chick booking form
                var model = new HomePageViewModel
                {
                    GalleryItems = _homeService.GetGalleryItems(),
                    ChickBooking = new ChickBooking()
                };
                return View(model); // Load the homepage with model data
            }
            catch (Exception ex)
            {
                // Log the error and redirect to error page
                _logger.LogError(ex, "Error loading home page.");
                TempData["Error"] = "Something went wrong while loading the homepage.";
                return RedirectToAction("Error");
            }
        }

        // Vaccine information page
        [Route("/vaccine")]
        public IActionResult Vaccine()
        {
            try
            {
                return View(); // Load Vaccine.cshtml
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading Vaccine page.");
                return RedirectToAction("Error");
            }
        }

        // Vaccine planner page
        [Route("/vaccineplanner")]
        public IActionResult VaccinePlanner()
        {
            try
            {
                return View(); // Load VaccinePlanner.cshtml
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading VaccinePlanner page.");
                return RedirectToAction("Error");
            }
        }

        // Aseel chicken breed page
        [Route("/aseel")]
        public IActionResult Aseel()
        {
            try
            {
                return View(); // Load Aseel.cshtml
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading Aseel page.");
                return RedirectToAction("Error");
            }
        }

        // Hatchery information page
        [Route("/hatchery")]
        public IActionResult Hatchery()
        {
            try
            {
                return View(); // Load Hatchery.cshtml
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading Hatchery page.");
                return RedirectToAction("Error");
            }
        }

        // Racing Homer breed page
        [Route("/racinghomer")]
        public IActionResult RacingHomer()
        {
            try
            {
                return View(); // Load RacingHomer.cshtml
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading RacingHomer page.");
                return RedirectToAction("Error");
            }
        }

        // Egg product page
        [Route("/egg")]
        public IActionResult Egg()
        {
            try
            {
                return View(); // Load Egg.cshtml
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading Egg page.");
                return RedirectToAction("Error");
            }
        }

        // Logout function - clear session and cookies
        public IActionResult Logout()
        {
            try
            {
                HttpContext.Session.Clear(); // Clear session data
                Response.Cookies.Delete("Token"); // Remove saved token from cookies
                return RedirectToAction("Index"); // Go back to homepage
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during logout.");
                return RedirectToAction("Error");
            }
        }

        // Dashboard for logged-in users
        [HttpGet]
        public IActionResult Dashboard()
        {
            try
            {
                var token = Request.Cookies["Token"]; // Get token from browser cookies

                // If token not found, user needs to log in
                if (string.IsNullOrEmpty(token))
                    return RedirectToAction("Login", "Auth");

                // Validate the token and get user info
                var principal = TokenHelper.ValidateToken(token, _config);
                if (principal == null)
                    return RedirectToAction("Login", "Auth");

                // Get email from token and find user
                var email = principal.FindFirst(ClaimTypes.Email)?.Value;
                var user = _homeService.GetUserByEmail(email);
                if (user == null)
                    return RedirectToAction("Login", "Auth");

                return View("Dashboard", user); // Show the dashboard with user data
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading dashboard.");
                return RedirectToAction("Error");
            }
        }

        // Error view when something goes wrong
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            // Return Error.cshtml with request ID info
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
    }
}
