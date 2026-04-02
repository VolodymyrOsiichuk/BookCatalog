namespace BookCatalog.Models.Entitites;



public class Comment
{
    public int Id { get; set; }

    public string Content { get; set; } = default!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public int BookId { get; set; }
    public Book Book { get; set; } = default!;

    public int UserId { get; set; }
    public User User { get; set; } = default!;
}