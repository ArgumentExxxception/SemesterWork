using HW2Sem.Entities;
using HW2Sem.Repositories.Interfaces;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure1.Repositories;

public class UserRepository: IUserRepository
{
    private readonly Context _dbContext;

    public UserRepository(Context dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<User?> GetUserByIdAsync(int id)
    {
        return await _dbContext.Users.FindAsync(id);
    }

    public async Task<IReadOnlyList<User>> GetAllUsersAsync()
    {
        var allUsers = await _dbContext.Users.Take(_dbContext.Set<User>().Count()).ToListAsync();
        return allUsers;
    }

    public async Task<User> AddUserAsync(User user)
    {
        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
        var newUser = await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();
        user.UserId = newUser.Entity.UserId;
        return newUser.Entity;
    }
    

    public async Task UpdateUserAsync(User user)
    {
        _dbContext.Entry(user).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }



    public async Task DeleteUserAsync(int id)
    {
        var user = await _dbContext.Set<User>().FindAsync(id);
        _dbContext.Set<User>().Remove(user!);
        await _dbContext.SaveChangesAsync();
    }

}