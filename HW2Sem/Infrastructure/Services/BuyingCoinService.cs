using Core1.Interfaces;
using HW2Sem.Entities;
using HW2Sem.Exceptions;
using HW2Sem.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace Infrastructure1.Services;

public class BuyingCoinService: IBuyingСoinService
{
    private readonly IUserCoinsRepository _userCoinsRepository;
    private readonly ILogger<BuyingCoinService> _logger;
    private readonly IUserRepository _userRepository;
    private readonly IPurchaseRepository _purchaseRepository;

    public BuyingCoinService(IUserRepository userRepository, IPurchaseRepository purchaseRepository, ILogger<BuyingCoinService> logger, IUserCoinsRepository userCoinsRepository)
    {
        _userRepository = userRepository;
        _purchaseRepository = purchaseRepository;
        _logger = logger;
        _userCoinsRepository = userCoinsRepository;
    }
    
    public async Task BuyCoin(int id,double amount, double currentPrice,string coinName)
    {
        var user = await _userRepository.GetUserByIdAsync(id);
        if (user!=null)
        {
            if (user.Balance >= currentPrice)
            {
                user.Balance -= amount * currentPrice;
                await _userRepository.UpdateUserAsync(user);
                var purchase = new Purchase()
                {
                    Id = new int(),
                    UserId = user.UserId,
                    Price = currentPrice,
                    CoinName = coinName,
                    CoinAmount = amount
                };
                var result = await _purchaseRepository.AddPurchaseAsync(purchase);
                if (result != null)
                {
                    _logger.LogInformation($"Пользователь - {user.Username} id: {user.UserId} купил " +
                                           $"{amount} {coinName}. Общая стоимость {amount*currentPrice}");
                    var userCoins = new UserCoins
                    {
                        CoinName = coinName,
                        Count = amount,
                        User = user,
                        UserId = user.UserId
                    };
                    await _userCoinsRepository.Add(userCoins);
                    return;
                }

                throw new Exception("Неудалось добавить покупку! Попробуйте снова");
            }
            throw new BadRequest("Недостаточно средств! Полоните баланс");
        }
        throw new Exception("Произошла какая то ошибка. Попробуйте снова");
    }
    
    public async Task<UserCoins> CoinsSell(int id,string coinName,double count, double price)
    {
        var user = await _userRepository.GetUserByIdAsync(id);
        var userCoins =await _userCoinsRepository.GetUserCoinsByUserId(id, coinName);
        if (userCoins == null || userCoins.Count < count)
        {
            throw new BadRequest("У вас нет такого коина или их количество меньше чем вы хотите продать");
        }
        
        userCoins.Count -= count;
        user.Balance += count * price;
        await _userCoinsRepository.UpdateUserCoinsAsync(userCoins);
        if (userCoins.Count == 0)
        {
            await _userCoinsRepository.DeleteEmptyCoin();
        }
        await _userRepository.UpdateUserAsync(user);
        return userCoins;
    }
}