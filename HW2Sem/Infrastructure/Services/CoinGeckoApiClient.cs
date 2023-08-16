using Core1.Interfaces;

namespace Infrastructure1.Services;

public class CoinGeckoApiClient: ICoinGeckoApiClient
{
    private readonly HttpClient _httpClient;
    public CoinGeckoApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<HttpContent> GetAllCoinsPrice()
    {
        var req = new HttpRequestMessage(
            HttpMethod.Get,
            "https://api.coingecko.com/api/v3/coins/markets?vs_currency=usd&order=market_cap_desc&per_page=100&page=1&sparkline=false&locale=en")
        {
            Headers =
            {
                {
                    "user-agent",
                    "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/112.0.0.0 Safari/537.36"
                }
            }
        };

        var res = await _httpClient.SendAsync(req);
        return res.Content;
    }
}