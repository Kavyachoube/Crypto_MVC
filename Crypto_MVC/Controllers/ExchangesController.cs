using Crypto_MVC.Services;
using Microsoft.AspNetCore.Mvc;

public class ExchangesController : Controller
{
    private readonly CoinGeckoService _coinGeckoService;

    public ExchangesController(CoinGeckoService coinGeckoService)
    {
        _coinGeckoService = coinGeckoService;
    }

    public async Task<IActionResult> Index()
    {
        var exchanges = await _coinGeckoService.GetExchangesAsync();
        return View(exchanges);
    }
}
