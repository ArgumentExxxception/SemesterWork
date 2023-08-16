using HW2Sem.Entities;

namespace HW2Sem.Repositories.Interfaces;

public interface ICommentRepository
{
    Task<Comment?> GetCommentById(int id);
    public Task AddComment(Comment comment);
    public Task UpdateComment(Comment comment);
    public Task DeleteComment(int id);
}