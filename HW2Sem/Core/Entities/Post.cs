namespace HW2Sem.Entities;

public class Post
{
    public int Id { get; set; }
    public int AuthorId { get; set; }
    public string? PostHeader { get; set; }
    public string? PostText { get; set; }
    public DateTime PostedAt { get; set; }

    public Author? Author { get; set; }
    public List<Comment>? Comments { get; set; }

}