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
    public async Task<Exchange?> GetExchangeByIdAsync(string exchangeId)
    {
        return await FetchDataAsync<Exchange?>($"https://api.coingecko.com/api/v3/exchanges/{exchangeId}");
    }

    // Fetch coin details by coinId using Newtonsoft.Json
    // Fetch coin details by coinId using Newtonsoft.Json
    public async Task<Coin?> GetCoinDetailsAsync(string coinId)
    {
        try
        {
            // API Call
            var response = await _httpClient.GetAsync($"https://api.coingecko.com/api/v3/coins/{coinId}?localization=false&sparkline=true");

            // Handle API rate limit (429 Too Many Requests)
            if (response.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
            {
                Console.WriteLine("Rate limit exceeded. Retrying after 10 seconds...");
                await Task.Delay(10000); // 10 second wait
                return await GetCoinDetailsAsync(coinId); // Retry the request
            }

            // Handle other errors
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Error fetching coin data. Status Code: {response.StatusCode}");
                return null;
            }

            // Read and check content
            var content = await response.Content.ReadAsStringAsync();
            if (string.IsNullOrWhiteSpace(content))
            {
                Console.WriteLine("API returned empty content.");
                return null;
            }

            Console.WriteLine($"API Response: {content}");

            // Deserialize using Newtonsoft.Json
            var coin = JsonConvert.DeserializeObject<Coin>(content);

            if (coin == null)
            {
                Console.WriteLine("Failed to deserialize coin data.");
                return null;
            }

            return coin;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception occurred while fetching coin data: {ex.Message}");
            return null;
        }
    }


    // Generic API call handler using Newtonsoft.Json
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
