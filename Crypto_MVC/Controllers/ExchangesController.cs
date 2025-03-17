using Crypto_MVC.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

[Route("exchanges")]
public class ExchangesController : Controller
{
    private readonly CoinGeckoService _coinService;

    public ExchangesController(CoinGeckoService coinService)
    {
        _coinService = coinService;
    }

    // GET: /exchanges
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
            return View("Error", ex.Message);
        }
    }

    // GET: /exchanges/{id}
    // ✅ Fetch exchange details
    [HttpGet("{id}")]
    public async Task<IActionResult> Details(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            return BadRequest("Exchange ID is required.");
        }

        try
        {
            var exchange = await _coinService.GetExchangeByIdAsync(id);
            if (exchange == null)
            {
                return NotFound();
            }
            return View(exchange);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching exchange details: {ex.Message}");
            return View("Error", ex.Message);
        }
    }
}
