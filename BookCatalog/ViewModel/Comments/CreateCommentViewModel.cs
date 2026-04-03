using System.ComponentModel.DataAnnotations;

namespace BookCatalog.ViewModels.Comments;

public class CreateCommentViewModel
{
    [Required]
    [StringLength(1000)]
    public string Content { get; set; } = default!;

    [Required]
    public int BookId { get; set; }
}