namespace HW2Sem.Entities;

public class UserAuthor
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User? User { get; set; }
    public int AuthorId { get; set; }
    public Author? Author { get; set; }

}