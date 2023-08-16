using System.Text.Json.Serialization;

namespace HW2Sem.Models;

public record CoinsDailyInfo(
    string Id,
    [property:JsonPropertyName("price_change_24h")]double PriceChange24H,
    [property:JsonPropertyName("price_change_percentage_24h")]double PriceChangePercentage24H,
    [property:JsonPropertyName("high_24h")]double MaxPrice24H,
    [property:JsonPropertyName("current_price")]double CurrentPrice,
    [property:JsonPropertyName("low_24h")]double LowPrice24H);
