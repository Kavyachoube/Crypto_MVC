
using Crypto_MVC.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configure Database
//builder.Services.AddDbContext<ApplicationDbContext>(options =>
    //options.UseSqlServer(builder.Configuration.GetConnectionString("con")));

// Configure Authentication
//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
   /* .AddCookie(options =>
    {
        options.LoginPath = "/Home/Login";
        options.LogoutPath = "/Home/Logout";
        options.AccessDeniedPath = "/Home/Register";
    });
builder.Services.AddAuthorization();*/


// Register HttpClient for external API calls (e.g., CoinGeckoService)
builder.Services.AddHttpClient<CoinGeckoService>();

// Add Controllers and Views
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure exception handling for production
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Middleware setup
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();


// Configure Routes
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Run the application
app.Run();
