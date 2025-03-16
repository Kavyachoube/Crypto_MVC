using Microsoft.AspNetCore.Mvc;
using Crypto_MVC.Services;
using System.Threading.Tasks;

namespace Crypto_MVC.Controllers
{
    public class CoinsController : Controller
    {
        private readonly CoinGeckoService _coinGeckoService;

        public CoinsController(CoinGeckoService coinGeckoService)
        {
            _coinGeckoService = coinGeckoService;
        }

        public async Task<IActionResult> Index()
        {
            var coins = await _coinGeckoService.GetCoinsAsync();
            return View(coins);
        }
    }
}
