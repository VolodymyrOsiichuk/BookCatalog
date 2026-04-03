namespace BookCatalog.ViewModels.Books;

public class BookListItemViewModel
{
    public int Id { get; set; }

    public string Title { get; set; } = default!;
    public string ShortDescription { get; set; } = default!;
    public int PublishedYear { get; set; }
    public decimal Price { get; set; }

    public string GenreName { get; set; } = default!;
    public string AuthorName { get; set; } = default!;
}