using HW2Sem.Entities;

namespace Core1.Interfaces;

public interface IReplenishmentOfTheBalanceService
{
    public Task<User?> ReplenishmentBalance(int userId, int money);
}