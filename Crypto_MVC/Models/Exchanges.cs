using Newtonsoft.Json;

namespace Crypto_MVC.Models
{
    public class Exchange
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("trust_score")]
        public int? TrustScore { get; set; }

        [JsonProperty("trade_volume_24h_btc")]
        public decimal? TradeVolume24hBtc { get; set; }
    }
}
