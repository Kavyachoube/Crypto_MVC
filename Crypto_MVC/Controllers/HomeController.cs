using Crypto_MVC.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Security.Cryptography;
using System.Security.Claims;

namespace Crypto_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        RegisterModel RegObj = new RegisterModel();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                model.Password = RegisterModel.HashPassword(model.Password); // Hash Password

                bool isInserted = RegObj.Insert(model);

                if (isInserted)
                {
                    TempData["msg"] = "Registration successful! Please log in.";
                    return RedirectToAction("Login");
                }
                else
                {
                    ViewBag.ErrorMessage = "Registration failed. Username or Email may already exist.";
                }
            }
            return View(model);
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                string hashedPassword = RegisterModel.HashPassword(model.Password);

                using (SqlConnection con = new SqlConnection("Server=(localdb)\\MSSQLLocalDB; Database=CryptoVerseDB; Trusted_Connection=True"))
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Register WHERE Username=@Username AND Password=@Password AND Role=@Role", con);
                    cmd.Parameters.AddWithValue("@Username", model.Username);
                    cmd.Parameters.AddWithValue("@Password", hashedPassword);
                    cmd.Parameters.AddWithValue("@Role", model.Role);

                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows && reader.Read())
                    {
                        var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, model.Username),
                    new Claim(ClaimTypes.Role, model.Role)
                };

                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var authProperties = new AuthenticationProperties { IsPersistent = true };

                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                                                       new ClaimsPrincipal(claimsIdentity),
                                                       authProperties);

                        reader.Close();

                        cmd = new SqlCommand("UPDATE Register SET LastLoginDate = GETDATE() WHERE Username=@Username", con);
                        cmd.Parameters.AddWithValue("@Username", model.Username);
                        cmd.ExecuteNonQuery();

                        TempData["msg"] = "Login successful!";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Invalid credentials or role. Please try again.";
                    }
                    con.Close();
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }


    }
}
