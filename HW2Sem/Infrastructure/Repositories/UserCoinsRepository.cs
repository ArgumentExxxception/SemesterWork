using HW2Sem.Entities;
using HW2Sem.Repositories.Interfaces;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure1.Repositories;

public class UserCoinsRepository: IUserCoinsRepository
{
    private readonly Context _context;

    public UserCoinsRepository(Context context)
    {
        _context = context;
    }

    public async Task<List<UserCoins?>> GetListUserCoinsByUserId(int id)
    {
        var userCoins =  _context.UserCoins.Where(uc => uc.UserId == id).ToList();
        return userCoins;
    }

    public async Task DeleteEmptyCoin()
    {
        var emptyCoins = _context.UserCoins.Where(uc=>uc.Count == 0);
        _context.UserCoins.RemoveRange(emptyCoins);
        await _context.SaveChangesAsync();
    }

    public async Task<UserCoins> Add(UserCoins userCoins)
    {
        var newUserCoin = await _context.UserCoins.AddAsync(userCoins);
        if (newUserCoin != null)
        {
            await _context.SaveChangesAsync();
            return userCoins;
        }

        return null;
    }

    public async Task<UserCoins?> GetUserCoinsByUserId(int id, string coinName)
    {
        var userCoins =  _context.UserCoins.FirstOrDefault(uc => uc.UserId == id && uc.CoinName == coinName);
        if (userCoins==null)
        {
            return null;
        }

        return userCoins;
    }

    public async Task UpdateUserCoinsAsync(UserCoins userCoins)
    {
        _context.Entry(userCoins).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }
}