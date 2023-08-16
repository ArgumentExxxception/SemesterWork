using HW2Sem.Entities;

namespace Core1.Interfaces;

public interface IAuthorService
{
    public Task<Author?> AddAuthor(Author author);
}