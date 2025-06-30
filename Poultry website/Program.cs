using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Poultry_website.Data;

var builder = WebApplication.CreateBuilder(args);

// ✅ Configure SQL Server DB Context
builder.Services.AddDbContext<PoultryData>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// ✅ Add MVC Controllers + Views
builder.Services.AddControllersWithViews();

// ✅ Add Session Support
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Auto-expire after 30 mins of inactivity
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// ✅ Add Authentication using Cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Auth/Login"; // Redirect if unauthenticated
        options.AccessDeniedPath = "/Auth/AccessDenied";
        options.ExpireTimeSpan = TimeSpan.FromDays(7); // Cookie expiry
        options.SlidingExpiration = true;
    });

var app = builder.Build();

// ✅ Error handling & HTTPS
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();        // To load /wwwroot assets (CSS, JS, etc.)
app.MapStaticAssets();       // Optional extension if you're using custom static folders

// ✅ Middleware Order: Routing → Session → Auth → Endpoints
app.UseRouting();
app.UseSession();
app.UseAuthentication();     // Required before authorization
app.UseAuthorization();

// ✅ Define Route Map
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
