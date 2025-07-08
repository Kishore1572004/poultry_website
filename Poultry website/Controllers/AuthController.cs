// Required for authentication-related classes
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Poultry_website.Domain.Interfaces.Auth;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace Poultry_website.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        // Constructor injection for auth service
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        // GET: Show registration form
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // POST: Handle registration form submission
        [HttpPost]
        [ValidateAntiForgeryToken] // Prevent CSRF attacks
        public async Task<IActionResult> Register(string fullName, string email, string password, string confirmPassword)
        {
            try
            {
                // Check if passwords match
                if (password != confirmPassword)
                {
                    ModelState.AddModelError("", "Passwords do not match.");
                    return View(); // Return with error
                }

                // Call service to register user
                var result = await _authService.RegisterAsync(fullName, email, password, confirmPassword);

                if (result)
                {
                    // Success message
                    TempData["Success"] = "Registration successful. Please log in.";
                    return RedirectToAction("Login"); // Redirect to login page
                }

                ModelState.AddModelError("", "Registration failed.");
                return View();
            }
            catch (Exception ex)
            {
                // Catch and display unexpected errors
                ModelState.AddModelError("", $"An error occurred: {ex.Message}");
                return View();
            }
        }

        // GET: Show login form
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // POST: Handle login submission
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string email, string password)
        {
            try
            {
                // Call login service and receive user info and token
                var (success, token, userEmail, userPassword, userId, fullName) = await _authService.LoginAsync(email, password);

                if (!success)
                {
                    ModelState.AddModelError("", "Invalid credentials.");
                    return View(); // Show login again
                }

                // Prepare claims for logged-in user
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                    new Claim(ClaimTypes.Name, fullName),
                    new Claim(ClaimTypes.Email, userEmail)
                };

                // Create identity and set cookie auth scheme
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                // Set authentication cookie properties
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true, // Keep user logged in
                    ExpiresUtc = DateTimeOffset.UtcNow.AddDays(7) // Cookie expiry
                };

                // Sign in the user
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties
                );

                // Store token in browser cookie
                Response.Cookies.Append("AuthToken", token);

                TempData["Success"] = $"Welcome, {userEmail}!";
                return RedirectToAction("Index", "Home"); // Go to home
            }
            catch (Exception ex)
            {
                // Show any unexpected error
                ModelState.AddModelError("", $"An error occurred during login: {ex.Message}");
                return View();
            }
        }

        // POST: Log out the user
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            try
            {
                // Remove authentication cookie
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                // Remove custom auth token cookie
                Response.Cookies.Delete("AuthToken");

                TempData["Success"] = "Logged out successfully.";
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Logout failed: {ex.Message}";
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
