using HW2Sem.Entities;
using HW2Sem.Repositories.Interfaces;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure1.Repositories;

public class PurchaseRepository: IPurchaseRepository
{
    private readonly Context _dbContext;

    public PurchaseRepository(Context dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Purchase?> GetPurchaseByIdAsync(int id)
    {
        return await _dbContext.Purchases.FindAsync(id);
    }
    public async Task<List<Purchase>> GetPurchaseByUserIdAsync(int id)
    {
        var userPurchase = _dbContext.Purchases.Where(p => p.UserId == id).ToList();
        return userPurchase;
    }

    public async Task<IReadOnlyList<Purchase>> GetAllPurchaseAsync()
    {
        var allUsersPurchases = await _dbContext.Set<Purchase>().Take(_dbContext.Set<Purchase>().Count()).ToListAsync();
        return allUsersPurchases;
    }

    public async Task<Purchase> AddPurchaseAsync(Purchase purchase)
    {
        var newPurchase = await _dbContext.Set<Purchase>().AddAsync(purchase);
        await _dbContext.SaveChangesAsync();
        return newPurchase.Entity;
    }

    public async Task UpdatePurchaseAsync(Purchase purchase)
    {
        _dbContext.Entry(purchase).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }
}