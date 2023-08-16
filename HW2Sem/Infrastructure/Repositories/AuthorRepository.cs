using HW2Sem.Entities;
using HW2Sem.Repositories.Interfaces;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure1.Repositories;

public class AuthorRepository: IAuthorRepository
{
    private readonly Context _dbContext;

    public AuthorRepository(Context dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Author?> GetAuthorByIdAsync(int id)
    {
        return await _dbContext.Set<Author>().FindAsync(id);
    }

    public async Task<Author?> AddAuthorAsync(Author author)
    {
        var newAuthor = await _dbContext.Set<Author>().AddAsync(author);
        await _dbContext.SaveChangesAsync();
        newAuthor.Entity.RegistrationDate = DateTime.UtcNow;
        author.RegistrationDate = newAuthor.Entity.RegistrationDate;
        await _dbContext.SaveChangesAsync();
        author.Id = newAuthor.Entity.Id;
        return newAuthor.Entity;
    }

    public async Task UpdateAuthorAsync(Author author)
    {
        _dbContext.Entry(author).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAuthorAsync(int id)
    {
        var author = await _dbContext.Set<Author>().FindAsync(id);
        _dbContext.Set<Author>().Remove(author);
        await _dbContext.SaveChangesAsync();
    }
}