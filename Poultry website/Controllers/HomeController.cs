using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Poultry_website.Domain.Entities;
using Poultry_website.Domain.Interfaces.Home;
using Poultry_website.Helpers;
using Poultry_website.Models;
using System.Diagnostics;
using System.Security.Claims;
using System.Text.Json;

namespace Poultry_website.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<HomeController> _logger;
        private readonly IHomeService _homeService;
        private readonly IConfiguration _config;

        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

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
            var model = new HomePageViewModel
            {
                GalleryItems = _homeService.GetGalleryItems(),
                ChickBooking = new ChickBooking()
            };
            return View(model);
        }

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

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            Response.Cookies.Delete("Token");
            return RedirectToAction("Index");
        }

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
            var user = _homeService.GetUserByEmail(email);
            if (user == null)
                return RedirectToAction("Login", "Auth");

            return View("Dashboard", user);
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
