using BookCatalog.Data;
using BookCatalog.Models.Entitites;
using BookCatalog.Services.Interfaces;
using BookCatalog.ViewModels.Comments;
using Microsoft.EntityFrameworkCore;

namespace BookCatalog.Services;

public class CommentService : ICommentService
{
    private readonly AppDbContext _context;

    public CommentService(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(CreateCommentViewModel model, int userId)
    {
        var bookExists = await _context.Books.AnyAsync(x => x.Id == model.BookId);
        if (!bookExists)
        {
            return;
        }

        var comment = new Comment
        {
            Content = model.Content,
            CreatedAt = DateTime.UtcNow,
            BookId = model.BookId,
            UserId = userId
        };

        _context.Comments.Add(comment);
        await _context.SaveChangesAsync();
    }
}