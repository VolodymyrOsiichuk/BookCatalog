using System.ComponentModel.DataAnnotations;

namespace BookCatalog.ViewModels.Authors;

public class AuthorViewModel
{
    public int Id { get; set; }

    [Required]
    [StringLength(150)]
    public string FullName { get; set; } = default!;

    [Required]
    [StringLength(3000)]
    public string Biography { get; set; } = default!;

    [Required]
    [EmailAddress]
    [StringLength(150)]
    public string Email { get; set; } = default!;
}