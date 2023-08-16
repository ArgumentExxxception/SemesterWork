using HW2Sem.Entities;
using HW2Sem.Repositories.Interfaces;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure1.Repositories;

public class PostRepository: IPostRepository
{
    private readonly Context _dbContext;
    
    public PostRepository(Context dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Post?> GetPostByIdAsync(int id)
    {
        return await _dbContext.Set<Post>().FindAsync(id);
    }

    public async Task<IReadOnlyList<Post>> GetAllPosts()
    {
        var allPosts = await _dbContext.Set<Post>().Take(_dbContext.Set<Post>().Count()).ToListAsync();
        return allPosts;
    }

    public async Task AddPostAsync(Post post)
    {
        await _dbContext.Set<Post>().AddAsync(post);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdatePostAsync(Post post)
    {
        _dbContext.Entry(post).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeletePostAsync(int id)
    {
        var post = await _dbContext.Set<Post>().FindAsync(id);
        _dbContext.Set<Post>().Remove(post);
        await _dbContext.SaveChangesAsync();
    }
}