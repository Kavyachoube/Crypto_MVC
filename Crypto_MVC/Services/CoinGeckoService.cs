using Crypto_MVC.Models;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Newtonsoft.Json;

public class CoinGeckoService
{
    private readonly HttpClient _httpClient;

    public CoinGeckoService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.DefaultRequestHeaders.Add("User-Agent", "CryptoMVCApp");
    }

    // Fetch Coins
   // public async Task<List<Coin>> GetCoinsAsync()
   // {
    //    return await FetchDataAsync<List<Coin>>("https://api.coingecko.com/api/v3/coins/markets?vs_currency=usd");
  //  }

    // Fetch all exchanges
    public async Task<List<Exchange>> GetExchangesAsync()
    {
        return await FetchDataAsync<List<Exchange>>("https://api.coingecko.com/api/v3/exchanges");
    }

    // Fetch exchange by ID
    public async Task<Exchange?> GetExchangeByIdAsync(string exchangeId)
    {
        return await FetchDataAsync<Exchange?>($"https://api.coingecko.com/api/v3/exchanges/{exchangeId}");
    }

    // Fetch coin details by coinId using Newtonsoft.Json
    // Fetch coin details by coinId using Newtonsoft.Json
    public async Task<List<Coin>> GetCoinsAsync()
    {
        return await FetchDataAsync<List<Coin>>("https://api.coingecko.com/api/v3/coins/markets?vs_currency=usd");
    }

    // ✅ For Details page (Full Data)
    public async Task<CoinDetail?> GetCoinDetailsAsync(string coinId)
    {
        try
        {
            var url = $"https://api.coingecko.com/api/v3/coins/{coinId}?localization=false&sparkline=true";
            var response = await _httpClient.GetAsync(url);

            if (response.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
            {
                Console.WriteLine("Rate limit exceeded. Retrying after 10 seconds...");
                await Task.Delay(10000);
                return await GetCoinDetailsAsync(coinId);
            }

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Error fetching coin data. Status Code: {response.StatusCode}");
                return null;
            }

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<CoinDetail>(content);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception occurred while fetching coin data: {ex.Message}");
            return null;
        }
    }

    // Generic API handler
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
            return new T();
        }
    }
}