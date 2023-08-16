namespace HW2Sem.Entities;

public class Comment
{
    public int Id { get; set; }
    public int PostId { get; set; }
    public int UserId { get; set; }
    public string? CommentText { get; set; }
    public DateTime CommentedAt { get; set; }

    public Post? Post { get; set; }
    public User? User { get; set; }

}