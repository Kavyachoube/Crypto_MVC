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
    [HttpGet("/Coins/Details/{id}")]
    public async Task<IActionResult> Details(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            return NotFound();
        }

        var coin = await _coinService.GetCoinDetailsAsync(id);

        if (coin == null)
        {
            return NotFound();
        }

        ViewBag.SparklineJson = JsonConvert.SerializeObject(coin.Sparkline.Price);
        return View(coin);
    }
}