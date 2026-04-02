namespace BookCatalog.Models.Entitites;


public class Author
{
    public int Id { get; set; }

    public string FullName { get; set; } = default!;
    public string Biography { get; set; } = default!;
    public string Email { get; set; } = default!;

    public ICollection<Book> Books { get; set; } = new List<Book>();
}