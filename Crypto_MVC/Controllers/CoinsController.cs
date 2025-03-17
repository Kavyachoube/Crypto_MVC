using Crypto_MVC.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

[Route("coins")]
public class CoinsController : Controller
{
    private readonly CoinGeckoService _coinService;

    public CoinsController(CoinGeckoService coinService)
    {
        _coinService = coinService;
    }

    [HttpGet("")]
    public async Task<IActionResult> Index()
    {
        try
        {
            var coins = await _coinService.GetCoinsAsync();
            return View(coins);
        }
        catch (Exception ex)
        {
            // Log error and return a friendly message
            Console.WriteLine($"Error fetching coins: {ex.Message}");
            return View("Error");
        }
    }
}
