using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Poultry_website.Domain.Interfaces.Auth;
using Poultry_website.DataAccess;
using Poultry_website.Domain;
using Poultry_website.Domain.Interfaces.Booking;
using Poultry_website.Domain.Interfaces.Home;
using Poultry_website.Service;

var builder = WebApplication.CreateBuilder(args);

//  Add Database Context
builder.Services.AddDbContext<PoultryData>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//  Register Repositories (DataAccess Layer)
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IHomeRepository, HomeRepository>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();

//  Register Services (Service Layer)
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IHomeService, HomeService>();
builder.Services.AddScoped<IBookingService, BookingService>();

//  Add MVC Controllers and Views
builder.Services.AddControllersWithViews();

//  Add Session Support
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

//  Add Authentication (Cookie)
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Auth/Login";
        options.AccessDeniedPath = "/Auth/AccessDenied";
        options.ExpireTimeSpan = TimeSpan.FromDays(7);
        options.SlidingExpiration = true;
    });

var app = builder.Build();

//  Error Handling and HTTPS Redirection
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

//  Middleware Setup
app.UseRouting();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

//  Route Mapping
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
