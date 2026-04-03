using BookCatalog.ViewModels.Comments;

namespace BookCatalog.Services.Interfaces;

public interface ICommentService
{
    Task AddAsync(CreateCommentViewModel model, int userId);
}