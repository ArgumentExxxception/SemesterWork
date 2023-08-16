using Core1.Interfaces;
using HW2Sem.Entities;
using HW2Sem.Repositories.Interfaces;

namespace Infrastructure1.Services;

public class UserService: IUserService
{
    private readonly IUserCoinsRepository _coinsRepository;
    private readonly IPurchaseRepository _purchaseRepository;
    private readonly IUserRepository _userRepository;
    public UserService(IPurchaseRepository purchaseRepository, IUserCoinsRepository coinsRepository, IUserRepository userRepository)
    {
        _purchaseRepository = purchaseRepository;
        _coinsRepository = coinsRepository;
        _userRepository = userRepository;
    }

    public async Task<List<Purchase>> GetUserPurchaseById(int userId)
    {
        var userPurchase =await _purchaseRepository.GetPurchaseByUserIdAsync(userId);
        return userPurchase;
    }

    public async Task<List<UserCoins>?> GetUserCoinsById(int userId)
    {
        var userCoins =await _coinsRepository.GetListUserCoinsByUserId(userId);
        return userCoins;
    }

    public async Task<List<User>> GetUserById(int userId)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);
        return new List<User> { user! };
    }
}