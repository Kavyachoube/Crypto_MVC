using Microsoft.AspNetCore.Mvc;
using Crypto_MVC; 
using Crypto_MVC.Models;
using Crypto_MVC.Helpers;


namespace Crypto_MVC.Controllers
{
    public class WatchlistController : Controller
    {
        private readonly CoinGeckoService _coinService;

        public WatchlistController(CoinGeckoService coinService)
        {
            _coinService = coinService;
        }

        private const string SessionKey = "Watchlist";

        public async Task<IActionResult> Index()
        {
            var coinIds = HttpContext.Session.GetObject<List<string>>(SessionKey) ?? new List<string>();
            var watchlist = new List<CoinDetail>();

            foreach (var id in coinIds)
            {
                var coin = await _coinService.GetCoinDetailsAsync(id);
                if (coin != null)
                    watchlist.Add(coin);
            }

            return View(watchlist);
        }

        [HttpPost]
        public IActionResult Add(string id)
        {
            var coinIds = HttpContext.Session.GetObject<List<string>>(SessionKey) ?? new List<string>();

            if (!coinIds.Contains(id))
            {
                coinIds.Add(id);
                HttpContext.Session.SetObject(SessionKey, coinIds);
            }

            return RedirectToAction("Details", "Coins", new { id });
        }

        [HttpPost]
        public IActionResult Remove(string id)
        {
            var coinIds = HttpContext.Session.GetObject<List<string>>(SessionKey) ?? new List<string>();
            coinIds.Remove(id);
            HttpContext.Session.SetObject(SessionKey, coinIds);
            return RedirectToAction("Index");
        }
    }
}
