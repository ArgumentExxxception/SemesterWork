using HW2Sem.Entities;

namespace HW2Sem.Repositories.Interfaces;

public interface IPurchaseRepository
{
    Task<Purchase?> GetPurchaseByIdAsync(int id);
    
    public Task<IReadOnlyList<Purchase>> GetAllPurchaseAsync();
    Task<List<Purchase>> GetPurchaseByUserIdAsync(int id);
    
    public Task<Purchase> AddPurchaseAsync(Purchase purchase);
    
    public Task UpdatePurchaseAsync(Purchase purchase);
}