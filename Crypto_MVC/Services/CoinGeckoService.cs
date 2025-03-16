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
            string url = "https://api.coingecko.com/api/v3/coins/markets?vs_currency=usd&order=market_cap_desc&per_page=250&page=1&sparkline=false";
            string json = await GetApiResponseAsync(url);
            return JsonConvert.DeserializeObject<List<Coin>>(json);
        }

        public async Task<List<Exchange>> GetExchangesAsync()
        {
            string url = "https://api.coingecko.com/api/v3/exchanges?per_page=250&page=1";
            string json = await GetApiResponseAsync(url);
            return JsonConvert.DeserializeObject<List<Exchange>>(json);
        }
    }
}
