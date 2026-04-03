using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookCatalog.ViewModels.Books;

public class CreateBookViewModel
{
    [Required]
    [StringLength(200)]
    public string Title { get; set; } = default!;

    [Required]
    [StringLength(3000)]
    public string Description { get; set; } = default!;

    [Range(1000, 3000)]
    public int PublishedYear { get; set; }

    [Range(0, 100000)]
    public decimal Price { get; set; }

    [Range(1, int.MaxValue)]
    public int GenreId { get; set; }

    [Range(1, int.MaxValue)]
    public int AuthorId { get; set; }

    public List<SelectListItem> Genres { get; set; } = new();
    public List<SelectListItem> Authors { get; set; } = new();
}