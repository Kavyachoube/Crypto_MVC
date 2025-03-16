using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Crypto_MVC.Models;

namespace Crypto_MVC.Services
{
    public class CoinGeckoService
    {
        private readonly HttpClient _httpClient;

        public CoinGeckoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        private async Task<string> GetApiResponseAsync(string url)
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("User-Agent", "Mozilla/5.0");

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<List<Coin>> GetCoinsAsync()
        {
            var url = "https://api.coingecko.com/api/v3/coins/markets?vs_currency=usd&order=market_cap_desc&per_page=250&page=1&sparkline=false";
            var response = await GetApiResponseAsync(url);

            var coins = JsonConvert.DeserializeObject<List<Coin>>(response);
            return coins;
        }

        public async Task<List<Exchange>> GetExchangesAsync()
        {
            var url = "https://api.coingecko.com/api/v3/exchanges";
            var response = await GetApiResponseAsync(url);

            var exchanges = JsonConvert.DeserializeObject<List<Exchange>>(response);
            return exchanges;
        }
    }
}