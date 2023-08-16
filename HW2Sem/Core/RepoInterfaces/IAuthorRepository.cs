using HW2Sem.Entities;

namespace HW2Sem.Repositories.Interfaces;

public interface IAuthorRepository
{
    Task<Author?> GetAuthorByIdAsync(int id);
    public Task<Author?> AddAuthorAsync(Author author);
    public Task UpdateAuthorAsync(Author author);
    public Task DeleteAuthorAsync(int id);
}