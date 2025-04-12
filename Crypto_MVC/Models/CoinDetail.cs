using Newtonsoft.Json;
using System.Collections.Generic;

namespace Crypto_MVC.Models
{
    public class CoinDetail
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("image")]
        public ImageData ImageData { get; set; }

        public string Image => ImageData?.Large ?? "";

        [JsonProperty("market_cap_rank")]
        public int MarketCapRank { get; set; }

        [JsonProperty("market_data")]
        public MarketData MarketData { get; set; }

        [JsonProperty("description")]
        public Dictionary<string, string> Description { get; set; }

        [JsonProperty("links")]
        public Links Links { get; set; }

        [JsonProperty("sparkline_in_7d")]
        public SparklineData Sparkline { get; set; }

        public decimal CurrentPrice => MarketData?.CurrentPrice["usd"] ?? 0;
        public decimal? PriceChangePercentage24h => MarketData?.PriceChangePercentage24h;
        public long MarketCap => (long)(MarketData?.MarketCap["usd"] ?? 0);
        public long TotalVolume => (long)(MarketData?.TotalVolume["usd"] ?? 0);
        public long CirculatingSupply => (long)(MarketData?.CirculatingSupply ?? 0);
    }

    public class ImageData
    {
        [JsonProperty("large")]
        public string Large { get; set; }
    }

    public class MarketData
    {
        [JsonProperty("current_price")]
        public Dictionary<string, decimal> CurrentPrice { get; set; }

        [JsonProperty("price_change_percentage_24h")]
        public decimal? PriceChangePercentage24h { get; set; }

        [JsonProperty("market_cap")]
        public Dictionary<string, decimal> MarketCap { get; set; }

        [JsonProperty("total_volume")]
        public Dictionary<string, decimal> TotalVolume { get; set; }

        [JsonProperty("circulating_supply")]
        public decimal? CirculatingSupply { get; set; }
    }

    public class Links
    {
        [JsonProperty("homepage")]
        public List<string> Homepage { get; set; }

        [JsonProperty("twitter_screen_name")]
        public string Twitter { get; set; }

        [JsonProperty("repos_url")]
        public Repos Repos { get; set; }
    }

    public class Repos
    {
        [JsonProperty("github")]
        public List<string> Github { get; set; }
    }

    public class SparklineData
    {
        [JsonProperty("price")]
        public List<double> Price { get; set; }
    }
}
