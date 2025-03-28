using Microsoft.AspNetCore.Mvc;

namespace Crypto_MVC.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
