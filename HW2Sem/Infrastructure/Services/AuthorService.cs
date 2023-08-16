using Core1.Interfaces;
using HW2Sem.Entities;
using HW2Sem.Repositories.Interfaces;

namespace Infrastructure1.Services;

public class AuthorService: IAuthorService
{
    private readonly IAuthorRepository _authorRepository;

    public AuthorService(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
    }

    public async Task<Author?> AddAuthor(Author author)
    {
        return await _authorRepository.AddAuthorAsync(author);
    }
}