using Crypto_MVC.Models;
using Microsoft.AspNetCore.Mvc;
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

    // ✅ Fetch all exchanges
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

    // ✅ Fetch exchange details by ID
    [HttpGet("{id}")]
    public async Task<IActionResult> Details(string id)
    {
        var exchange = await _coinService.GetExchangeByIdAsync(id);
        if (exchange == null)
        {
            return NotFound("Exchange not found.");
        }

        var exchangeModel = new ExchangeModel
        {
            Id = exchange.Id,
            Name = exchange.Name,
            Description = exchange.Description,
            Url = exchange.Url,
            Image = exchange.Image,
            TrustScore = exchange.TrustScore,
            YearEstablished = exchange.YearEstablished,
            TradeVolumeBTC = exchange.TradeVolumeBTC,
            Twitter = exchange.Twitter,
            Facebook = exchange.Facebook,
            Tickers = exchange.Tickers ?? new List<TradingCoin>() // Null Check and Initialize
        };

        return View(exchangeModel);
    }
    }


