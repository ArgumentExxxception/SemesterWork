using HW2Sem.Entities;

namespace HW2Sem.Repositories.Interfaces;

public interface IPostRepository
{
    Task<Post?> GetPostByIdAsync(int id);
    Task<IReadOnlyList<Post>> GetAllPosts();
    public Task AddPostAsync(Post post);
    public Task UpdatePostAsync(Post post);
    public Task DeletePostAsync(int id);
}