using System.ComponentModel.DataAnnotations;

namespace BookCatalog.ViewModels.Genres;

public class GenreViewModel
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = default!;

    [Required]
    [StringLength(1000)]
    public string Description { get; set; } = default!;
}