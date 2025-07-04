using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Logging;
using Poultry_website.Domain.Entities;
using Poultry_website.Domain.Interfaces.Home;
using Poultry_website.Helpers;
using Poultry_website.Models;
using System.Diagnostics;
using System.Security.Claims;
using System;

namespace Poultry_website.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<HomeController> _logger;
        private readonly IHomeService _homeService;
        private readonly IConfiguration _config;

        //private static readonly JsonSerializerOptions _jsonOptions = new()
        //{
        //    PropertyNameCaseInsensitive = true
        //};

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

        public IActionResult Index()
        {
            try
            {
                var model = new HomePageViewModel
                {
                    GalleryItems = _homeService.GetGalleryItems(),
                    ChickBooking = new ChickBooking()
                };
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading home page.");
                TempData["Error"] = "Something went wrong while loading the homepage.";
                return RedirectToAction("Error");
            }
        }

        [Route("/vaccine")]
        public IActionResult Vaccine()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading Vaccine page.");
                return RedirectToAction("Error");
            }
        }

        [Route("/vaccineplanner")]
        public IActionResult VaccinePlanner()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading VaccinePlanner page.");
                return RedirectToAction("Error");
            }
        }

        [Route("/aseel")]
        public IActionResult Aseel()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading Aseel page.");
                return RedirectToAction("Error");
            }
        }

        [Route("/hatchery")]
        public IActionResult Hatchery()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading Hatchery page.");
                return RedirectToAction("Error");
            }
        }

        [Route("/racinghomer")]
        public IActionResult RacingHomer()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading RacingHomer page.");
                return RedirectToAction("Error");
            }
        }

        [Route("/egg")]
        public IActionResult Egg()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading Egg page.");
                return RedirectToAction("Error");
            }
        }

        public IActionResult Logout()
        {
            try
            {
                HttpContext.Session.Clear();
                Response.Cookies.Delete("Token");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during logout.");
                return RedirectToAction("Error");
            }
        }

        [HttpGet]
        public IActionResult Dashboard()
        {
            try
            {
                var token = Request.Cookies["Token"];
                if (string.IsNullOrEmpty(token))
                    return RedirectToAction("Login", "Auth");

                var principal = TokenHelper.ValidateToken(token, _config);
                if (principal == null)
                    return RedirectToAction("Login", "Auth");

                var email = principal.FindFirst(ClaimTypes.Email)?.Value;
                var user = _homeService.GetUserByEmail(email);
                if (user == null)
                    return RedirectToAction("Login", "Auth");

                return View("Dashboard", user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading dashboard.");
                return RedirectToAction("Error");
            }
        }

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
