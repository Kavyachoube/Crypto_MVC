using Crypto_MVC.Models; // Ensure correct namespace
public class ExchangeModel
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Country { get; set; }
    public string Image { get; set; }
    public string Url { get; set; }
    public int TrustScore { get; set; }
    public int YearEstablished { get; set; }
    public double TradeVolumeBTC { get; set; }
    public string Description { get; set; }
    public string Twitter { get; set; }
    public string Facebook { get; set; }
    public List<TradingCoin> Tickers { get; set; }
}
