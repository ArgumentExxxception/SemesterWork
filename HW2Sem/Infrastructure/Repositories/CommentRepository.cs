using HW2Sem.Entities;
using HW2Sem.Repositories.Interfaces;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure1.Repositories;

public class CommentRepository: ICommentRepository
{
    private readonly Context _dbContext;

    public CommentRepository(Context dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Comment?> GetCommentById(int id)
    {
        return await _dbContext.Set<Comment>().FindAsync(id);
    }

    public async Task AddComment(Comment comment)
    {
        await _dbContext.Set<Comment>().AddAsync(comment);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateComment(Comment comment)
    {
        _dbContext.Entry(comment).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteComment(int id)
    {
        var comment = await _dbContext.Set<Comment>().FindAsync(id);
        _dbContext.Set<Comment>().Remove(comment!);
        await _dbContext.SaveChangesAsync();
    }
}