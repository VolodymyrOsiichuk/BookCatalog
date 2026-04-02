namespace BookCatalog.Models.Entitites;


public class Genre
{
    public int Id { get; set; }

    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;

    public ICollection<Book> Books = new List<Book>();
}