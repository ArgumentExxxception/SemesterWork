using HW2Sem.Entities;

namespace Core1.Interfaces;

public interface IUserService
{
    Task<List<Purchase>> GetUserPurchaseById(int userId);
    Task<List<UserCoins>?> GetUserCoinsById(int userId);
    Task<List<User>> GetUserById(int userId);
}