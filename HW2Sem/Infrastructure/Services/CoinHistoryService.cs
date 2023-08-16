using System.Text.Json;
using Core1.Interfaces;
using HW2Sem.Models;
using Microsoft.Extensions.Logging;

namespace Infrastructure1.Services;

public class CoinHistoryService: ICoinHistoryService
{
    private readonly ILogger<ICoinHistoryService> _logger;
    private readonly HttpClient? _client;
    private readonly JsonSerializerOptions _options = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };
    public CoinHistoryService(HttpClient? client, ILogger<ICoinHistoryService> logger)
    {
        _client = client;
        _logger = logger;
    }
    public async Task<GetHistoryResponse?> GetByIdCoinHistory(string id, CancellationToken cancellationToken)
    {
        var toDate = (int)(DateTime.Now - DateTime.UnixEpoch).TotalSeconds;
        var fromDate = toDate - 21600;
        var result = await _client?.GetAsync($"https://api.coingecko.com/api/v3/coins/{id}/market_chart/range?vs_currency=usd&from={fromDate}&to={toDate}", cancellationToken)!;
        return await JsonSerializer.DeserializeAsync<GetHistoryResponse>(
            await result.Content.ReadAsStreamAsync(cancellationToken),
            _options,
            cancellationToken: cancellationToken);
    }
    
    public async Task<IReadOnlyList<CoinsDailyInfo>?> GetCoinsDailyInfoById(string id,CancellationToken token)
    {
        var req = new HttpRequestMessage(
            HttpMethod.Get,
            $"https://api.coingecko.com/api/v3/coins/markets?vs_currency=usd&ids={id}&order=market_cap_desc&per_page=100&page=1&sparkline=false")
        {
            Headers =
            {
                {
                    "user-agent",
                    "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/112.0.0.0 Safari/537.36"
                }
            }
        };
        
        _logger.LogInformation("get coins daily info");
        var res = await _client.SendAsync(req);
        return await JsonSerializer.DeserializeAsync<List<CoinsDailyInfo>?>(await res.Content.ReadAsStreamAsync(token),_options,cancellationToken:token);
    }
    
    public async Task<List<CoinOHLC>> GetOhlcInfoById(string id, CancellationToken token)
    {
        var result = await _client.GetAsync($"https://api.coingecko.com/api/v3/coins/{id}/ohlc?vs_currency=usd&days=1", token);
        var ohlcList = await JsonSerializer.DeserializeAsync<List<List<double>>>(await result.Content.ReadAsStreamAsync(token),
            _options,
            cancellationToken: token);
        return ohlcList.Select(s=> new CoinOHLC(DateTimeOffset.FromUnixTimeMilliseconds((long)s[0]).DateTime, s[1], s[2], s[3], s[4])).ToList();
    }
}