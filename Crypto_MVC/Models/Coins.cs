using Newtonsoft.Json;
using System.Collections.Generic;

namespace Crypto_MVC.Models
{
    public class Coin
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }

        [JsonProperty("current_price")]
        public decimal CurrentPrice { get; set; }

        [JsonProperty("price_change_percentage_24h")]
        public decimal? PriceChangePercentage24h { get; set; }

        [JsonProperty("market_cap")]
        public long MarketCap { get; set; }

        [JsonProperty("total_volume")]
        public long TotalVolume { get; set; }

        [JsonProperty("circulating_supply")]
        public long CirculatingSupply { get; set; }

        [JsonProperty("description")]
        public Dictionary<string, string> Description { get; set; }

        [JsonProperty("links")]
        public Links Links { get; set; }

        [JsonProperty("market_cap_rank")]
        public int MarketCapRank { get; set; }

        [JsonProperty("sparkline_in_7d")]
        public SparklineData Sparkline { get; set; }
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
