using HW2Sem.Models;

namespace HW2Sem.Entities;

public class Author
{
    public int Id { get; set; }
    public string AuthorName { get; set; } = null!;
    public string Password { get; set; } = null!;
    public DateTime RegistrationDate { get; set; }
    public string? Description { get; set; }
    
    public List<Post>? Posts { get; set; }
    public List<UserAuthor>? Users { get; set; }
}