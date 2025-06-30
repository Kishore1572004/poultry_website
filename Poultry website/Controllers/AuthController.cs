
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Poultry_website.Data;
using Poultry_website.Helpers;
using Poultry_website.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Poultry_website.Controllers
{
    public class AuthController : Controller
    {
        private readonly PoultryData _context;
        private readonly IConfiguration _config;

        public AuthController(PoultryData context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        // GET: /Auth/Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // POST: /Auth/Register
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Register(RegisterViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            if (_context.Users.Any(u => u.Email == vm.Email))
            {
                ModelState.AddModelError(nameof(vm.Email), "Email is already registered.");
                return View(vm);
            }

            var user = new User
            {
                FullName = vm.FullName,
                Email = vm.Email,
                PasswordHash = PasswordHelper.HashPassword(vm.Password),
                CreatedAt = DateTime.Now
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            TempData["Success"] = "Registration successful! Please log in.";
            return RedirectToAction("Login");
        }


        // GET: /Auth/Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Auth/Login
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var user = _context.Users.FirstOrDefault(u => u.Email == vm.Email);
            if (user == null || !PasswordHelper.VerifyPassword(vm.Password, user.PasswordHash))
            {
                ModelState.AddModelError(string.Empty, "Invalid email or password.");
                return View(vm);
            }

            // ✅ Generate JWT token
            var token = JwtHelper.GenerateToken(user.UserId, user.Email, _config);

            // ✅ Save token cookie
            Response.Cookies.Append("Token", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = false,
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddDays(7)
            });

            // ✅ Add claims-based authentication for HttpContext.User.Identity
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.FullName)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            TempData["Success"] = "Login successful!";
            return RedirectToAction("Index", "Home");
        }

        // POST: /Auth/Logout
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("Token");
            TempData["Success"] = "Logged out successfully.";
            return RedirectToAction("Index", "Home");
        }
    }
}
