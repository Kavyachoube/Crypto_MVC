using Microsoft.AspNetCore.Mvc;

namespace Crypto_MVC.Controllers
{
    public class AuthController : Controller
    {
        // Display Sign In Page
        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        // Display Sign Up Page
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }
    }
}
