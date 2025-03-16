namespace Crypto_MVC.Models
{
    public class Exchange
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int TrustScoreRank { get; set; }
        public decimal TradeVolume24hBtc { get; set; }
        public string Country { get; set; }
        public int YearEstablished { get; set; }
        public string Url { get; set; }
    }
}
