using BookCatalog.ViewModels.Comments;

namespace BookCatalog.ViewModels.Books;

public class BookDetailsViewModel
{
    public int Id { get; set; }

    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public int PublishedYear { get; set; }
    public decimal Price { get; set; }
    public DateTime CreatedAt { get; set; }

    public string GenreName { get; set; } = default!;
    public string AuthorName { get; set; } = default!;
    public string AuthorEmail { get; set; } = default!;
    public string AuthorBiography { get; set; } = default!;

    public List<CommentItemViewModel> Comments { get; set; } = new();
}