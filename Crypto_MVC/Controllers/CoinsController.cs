using Microsoft.AspNetCore.Mvc;
using Crypto_MVC.Models;
using System;
using System.Threading.Tasks;
using Newtonsoft.Json;

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
    [HttpGet("Details/{coinId}")]
    public async Task<IActionResult> Details(string coinId)
    {
        var coin = await _coinService.GetCoinDetailsAsync(coinId);

        if (coin == null)
        {
            return Content("Coin details not found.");
        }

        return View(coin);
    }
}