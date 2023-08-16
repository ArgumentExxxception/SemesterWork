using Core1.Interfaces;
using HW2Sem.Entities;
using HW2Sem.Exceptions;
using HW2Sem.Repositories.Interfaces;

namespace Infrastructure1.Services;

public class ReplenishmentOfTheBalance: IReplenishmentOfTheBalanceService 
{
    public ReplenishmentOfTheBalance(IUserRepository userRepository)
    {
        UserRepository = userRepository;
    }
    
    private IUserRepository UserRepository { get; set; }
    public async Task<User?> ReplenishmentBalance(int userId ,int money)
    {
        var user = await UserRepository.GetUserByIdAsync(userId);
        if (user!=null)
        {
            user.Balance += money;
            await UserRepository.UpdateUserAsync(user);
            return user;
        }
        throw new BadRequest("Не удалось найти пользователя! Попробуйте снова");
    }
}