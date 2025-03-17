using Crypto_MVC.Services;
using Microsoft.AspNetCore.Mvc;

[Route("exchanges")]
public class ExchangesController : Controller
{
    private readonly CoinGeckoService _coinService;

    public ExchangesController(CoinGeckoService coinService)
    {
        _coinService = coinService;
    }

    [HttpGet("")]
    public async Task<IActionResult> Index()
    {
        try
        {
            var exchanges = await _coinService.GetExchangesAsync();
            return View(exchanges);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching exchanges: {ex.Message}");
            return View("Error");
        }
    }
}
