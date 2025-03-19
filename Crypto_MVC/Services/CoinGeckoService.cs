using Crypto_MVC.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

public class CoinGeckoService
{
    private readonly HttpClient _httpClient;

    public CoinGeckoService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.DefaultRequestHeaders.Add("User-Agent", "CryptoMVCApp");
    }

    // Fetch Coins
    public async Task<List<Coin>> GetCoinsAsync()
    {
        return await FetchDataAsync<List<Coin>>("https://api.coingecko.com/api/v3/coins/markets?vs_currency=usd");
    }

    // Fetch all exchanges
    public async Task<List<Exchange>> GetExchangesAsync()
    {
        return await FetchDataAsync<List<Exchange>>("https://api.coingecko.com/api/v3/exchanges");
    }

    // Fetch exchange by ID
    public async Task<Exchange> GetExchangeByIdAsync(string exchangeId)
    {
        return await FetchDataAsync<Exchange>($"https://api.coingecko.com/api/v3/exchanges/{exchangeId}");
    }

    // Generic API call handler
    private async Task<T> FetchDataAsync<T>(string url) where T : new()
    {
        try
        {
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(content) ?? new T();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching data from {url}: {ex.Message}");
            return new T(); // Returns an empty object of type T
        }
    }
}
