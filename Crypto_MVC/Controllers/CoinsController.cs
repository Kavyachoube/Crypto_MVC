using Microsoft.AspNetCore.Mvc;
using Crypto_MVC.Models;
using System;
using System.Threading.Tasks;
using Newtonsoft.Json;

[Route("Coins")]
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
            Console.WriteLine($"Error fetching coins: {ex.Message}");
            return View("Error", ex.Message);
        }
    }

    [HttpGet("Details/{id}")]
    public async Task<IActionResult> Details(string id)
    {
        if (string.IsNullOrEmpty(id))
            return NotFound("Coin ID cannot be empty.");

        var coin = await _coinService.GetCoinDetailsAsync(id);

        if (coin == null)
            return NotFound($"Coin with ID '{id}' not found.");

        ViewBag.SparklineJson = JsonConvert.SerializeObject(coin.Sparkline?.Price);
        return View(coin);
    }
}
