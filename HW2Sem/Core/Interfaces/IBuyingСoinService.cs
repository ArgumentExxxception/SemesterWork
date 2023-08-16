using HW2Sem.Entities;

namespace Core1.Interfaces;

public interface IBuyingСoinService
{
    public Task BuyCoin(int id, double amount, double currentPrice, string coinName);
    Task<UserCoins> CoinsSell(int id, string coinName, double count, double price);
}