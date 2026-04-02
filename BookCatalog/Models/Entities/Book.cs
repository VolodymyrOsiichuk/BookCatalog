namespace BookCatalog.Models.Entitites;


public class Book
{
    public int Id { get; set; }

    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public int PublishedYear { get; set; }
    public decimal Price { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public int GenreId { get; set; }
    public Genre Genre { get; set; } = default!;

    public int AuthorId { get; set; }
    public Author Author { get; set; } = default!;

    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
}