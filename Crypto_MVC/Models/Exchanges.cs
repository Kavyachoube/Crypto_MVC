using Newtonsoft.Json;

namespace Crypto_MVC.Models
{
    public class Exchange
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }

        [JsonProperty("trust_score_rank")]
        public int? TrustScoreRank { get; set; }

        [JsonProperty("trade_volume_24h_btc")]
        public double? TradeVolume24hBtc { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("year_established")]
        public int? YearEstablished { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
