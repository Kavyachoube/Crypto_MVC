using Microsoft.AspNetCore.Mvc;

public class CoinsController : Controller
{
    private readonly CoinGeckoService _coinService;

    public CoinsController(CoinGeckoService coinService)
    {
        _coinService = coinService;
    }

    // Fetch list of coins
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
}
