using HW2Sem.Entities;

namespace HW2Sem.Repositories.Interfaces;

public interface IUserRepository
{
    Task<User?> GetUserByIdAsync(int id);

    public Task<IReadOnlyList<User>> GetAllUsersAsync();
    
    public Task<User> AddUserAsync(User user);
    
    public Task UpdateUserAsync(User user);
    
}