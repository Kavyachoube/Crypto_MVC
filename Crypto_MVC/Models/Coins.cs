namespace Crypto_MVC.Models
{
    public class Coin
    {
        public string Id { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public decimal CurrentPrice { get; set; }
        public int MarketCapRank { get; set; }
        public long TotalVolume { get; set; }
        public decimal PriceChangePercentage24h { get; set; }
        public decimal MarketCap { get; set; }
    }
}
