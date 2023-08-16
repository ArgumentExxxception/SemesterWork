using HW2Sem.Models;

namespace Core1.Interfaces;

public interface ICoinHistoryService
{
    public Task<GetHistoryResponse?> GetByIdCoinHistory(string id, CancellationToken cancellationToken);

    public Task<IReadOnlyList<CoinsDailyInfo>?> GetCoinsDailyInfoById(string id, CancellationToken token);

    public Task<List<CoinOHLC>> GetOhlcInfoById(string id, CancellationToken token);
}