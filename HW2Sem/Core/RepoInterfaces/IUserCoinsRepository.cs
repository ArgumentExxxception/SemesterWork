using HW2Sem.Entities;

namespace HW2Sem.Repositories.Interfaces;

public interface IUserCoinsRepository
{
    Task<UserCoins> Add(UserCoins userCoins);
    Task UpdateUserCoinsAsync(UserCoins userCoins);
    Task<UserCoins?> GetUserCoinsByUserId(int id, string coinName);
    
    Task<List<UserCoins?>> GetListUserCoinsByUserId(int id);
    Task DeleteEmptyCoin();
}