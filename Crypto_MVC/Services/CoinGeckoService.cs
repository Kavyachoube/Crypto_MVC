using Crypto_MVC.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Crypto_MVC.Services
{
    public class CoinGeckoService
    {
        private readonly HttpClient _httpClient;

        public CoinGeckoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "CryptoMVCApp");
        }

        public async Task<List<Coin>> GetCoinsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("https://api.coingecko.com/api/v3/coins/markets?vs_currency=usd");
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Coin>>(content) ?? new List<Coin>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching coins: {ex.Message}");
                return new List<Coin>();
            }
        }

        public async Task<List<Exchange>> GetExchangesAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("https://api.coingecko.com/api/v3/exchanges");
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Exchange>>(content) ?? new List<Exchange>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching exchanges: {ex.Message}");
                return new List<Exchange>();
            }
        }
    }
}
