document.addEventListener("DOMContentLoaded", function () {
    fetch("https://api.coingecko.com/api/v3/coins/markets?vs_currency=usd&order=market_cap_desc&per_page=10&page=1&sparkline=false")
        .then(response => response.json())
        .then(data => {
            console.log("API Response Data:", data); // 🔍 See what's coming from API
        })
        .catch(error => console.error("Error fetching data:", error));
    data.forEach((coin) => {
        document.getElementById("coin-container").innerHTML +=
            <div class="coin-card">
                <div class="coin-header">
                    <img src="${coin.image}" alt="${coin.name}">
                        <h3>${coin.name} (${coin.symbol.toUpperCase()})</h3>
                </div>
                <p>Price: $${coin.current_price ?? "N/A"}</p>
                <p>Market Cap Rank: ${coin.market_cap_rank ?? "N/A"}</p>
                <p>Market Cap: $${coin.market_cap.toLocaleString() ?? "N/A"}</p>
                <p>24h Volume: $${coin.total_volume.toLocaleString() ?? "N/A"}</p>
                <p>24h Change: ${coin.price_change_percentage_24h?.toFixed(2) ?? "N/A"}%</p>
            </div>
            ;
    });
    var content = await response.Content.ReadAsStringAsync();
    return System.Text.Json.JsonSerializer.Deserialize < Coin > (content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

function createPost() {
    const title = document.getElementById('title').value;
    const content = document.getElementById('content').value;

    if (!title || !content) {
        alert("Both Title and Content are required!");
        return;
    }