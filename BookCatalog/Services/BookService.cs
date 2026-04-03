using BookCatalog.Data;
using BookCatalog.Models.Entitites;
using BookCatalog.Services.Interfaces;
using BookCatalog.ViewModels.Books;
using BookCatalog.ViewModels.Comments;
using Microsoft.EntityFrameworkCore;

namespace BookCatalog.Services;

public class BookService : IBookService
{
    private readonly AppDbContext _context;
    private readonly IGenreService _genreService;
    private readonly IAuthorService _authorService;

    public BookService(
        AppDbContext context,
        IGenreService genreService,
        IAuthorService authorService)
    {
        _context = context;
        _genreService = genreService;
        _authorService = authorService;
    }

    public async Task<List<BookListItemViewModel>> GetAllAsync()
    {
        return await _context.Books
            .AsNoTracking()
            .Include(x => x.Genre)
            .Include(x => x.Author)
            .OrderByDescending(x => x.CreatedAt)
            .Select(x => new BookListItemViewModel
            {
                Id = x.Id,
                Title = x.Title,
                ShortDescription = x.Description.Length > 120
                    ? x.Description.Substring(0, 120) + "..."
                    : x.Description,
                PublishedYear = x.PublishedYear,
                Price = x.Price,
                GenreName = x.Genre.Name,
                AuthorName = x.Author.FullName
            })
            .ToListAsync();
    }

    public async Task<BookDetailsViewModel?> GetByIdAsync(int id)
    {
        return await _context.Books
            .AsNoTracking()
            .Include(x => x.Genre)
            .Include(x => x.Author)
            .Include(x => x.Comments)
                .ThenInclude(x => x.User)
            .Where(x => x.Id == id)
            .Select(x => new BookDetailsViewModel
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                PublishedYear = x.PublishedYear,
                Price = x.Price,
                CreatedAt = x.CreatedAt,
                GenreName = x.Genre.Name,
                AuthorName = x.Author.FullName,
                AuthorEmail = x.Author.Email,
                AuthorBiography = x.Author.Biography,
                Comments = x.Comments
                    .OrderByDescending(c => c.CreatedAt)
                    .Select(c => new CommentItemViewModel
                    {
                        Id = c.Id,
                        Content = c.Content,
                        CreatedAt = c.CreatedAt,
                        Username = c.User.Username
                    })
                    .ToList()
            })
            .FirstOrDefaultAsync();
    }

    public async Task<CreateBookViewModel> GetCreateViewModelAsync()
    {
        return new CreateBookViewModel
        {
            Genres = await _genreService.GetSelectListAsync(),
            Authors = await _authorService.GetSelectListAsync()
        };
    }

    public async Task<EditBookViewModel?> GetEditViewModelAsync(int id)
    {
        var book = await _context.Books
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

        if (book is null) return null;

        return new EditBookViewModel
        {
            Id = book.Id,
            Title = book.Title,
            Description = book.Description,
            PublishedYear = book.PublishedYear,
            Price = book.Price,
            GenreId = book.GenreId,
            AuthorId = book.AuthorId,
            Genres = await _genreService.GetSelectListAsync(),
            Authors = await _authorService.GetSelectListAsync()
        };
    }

    public async Task CreateAsync(CreateBookViewModel model)
    {
        var book = new Book
        {
            Title = model.Title,
            Description = model.Description,
            PublishedYear = model.PublishedYear,
            Price = model.Price,
            GenreId = model.GenreId,
            AuthorId = model.AuthorId,
            CreatedAt = DateTime.UtcNow
        };

        _context.Books.Add(book);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(EditBookViewModel model)
    {
        var book = await _context.Books.FirstOrDefaultAsync(x => x.Id == model.Id);
        if (book is null) return;

        book.Title = model.Title;
        book.Description = model.Description;
        book.PublishedYear = model.PublishedYear;
        book.Price = model.Price;
        book.GenreId = model.GenreId;
        book.AuthorId = model.AuthorId;

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var book = await _context.Books.FirstOrDefaultAsync(x => x.Id == id);
        if (book is null) return;

        _context.Books.Remove(book);
        await _context.SaveChangesAsync();
    }
}