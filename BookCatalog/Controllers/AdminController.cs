using BookCatalog.Services.Interfaces;
using BookCatalog.ViewModels.Authors;
using BookCatalog.ViewModels.Books;
using BookCatalog.ViewModels.Genres;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookCatalog.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly IBookService _bookService;
    private readonly IGenreService _genreService;
    private readonly IAuthorService _authorService;

    public AdminController(
        IBookService bookService,
        IGenreService genreService,
        IAuthorService authorService)
    {
        _bookService = bookService;
        _genreService = genreService;
        _authorService = authorService;
    }

    public IActionResult Dashboard()
    {
        return View();
    }

    public async Task<IActionResult> Books()
    {
        var books = await _bookService.GetAllAsync();
        return View(books);
    }

    [HttpGet]
    public async Task<IActionResult> CreateBook()
    {
        var model = await _bookService.GetCreateViewModelAsync();
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> CreateBook(CreateBookViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.Genres = await _genreService.GetSelectListAsync();
            model.Authors = await _authorService.GetSelectListAsync();
            return View(model);
        }

        await _bookService.CreateAsync(model);
        return RedirectToAction(nameof(Books));
    }

    [HttpGet]
    public async Task<IActionResult> EditBook(int id)
    {
        var model = await _bookService.GetEditViewModelAsync(id);
        if (model is null) return NotFound();

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> EditBook(EditBookViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.Genres = await _genreService.GetSelectListAsync();
            model.Authors = await _authorService.GetSelectListAsync();
            return View(model);
        }

        await _bookService.UpdateAsync(model);
        return RedirectToAction(nameof(Books));
    }

    [HttpPost]
    public async Task<IActionResult> DeleteBook(int id)
    {
        await _bookService.DeleteAsync(id);
        return RedirectToAction(nameof(Books));
    }

    public async Task<IActionResult> Genres()
    {
        var genres = await _genreService.GetAllAsync();
        return View(genres);
    }

    [HttpGet]
    public IActionResult CreateGenre()
    {
        return View(new GenreViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> CreateGenre(GenreViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        await _genreService.CreateAsync(model);
        return RedirectToAction(nameof(Genres));
    }

    [HttpGet]
    public async Task<IActionResult> EditGenre(int id)
    {
        var model = await _genreService.GetByIdAsync(id);
        if (model is null) return NotFound();

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> EditGenre(GenreViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        await _genreService.UpdateAsync(model);
        return RedirectToAction(nameof(Genres));
    }

    [HttpPost]
    public async Task<IActionResult> DeleteGenre(int id)
    {
        await _genreService.DeleteAsync(id);
        return RedirectToAction(nameof(Genres));
    }

    public async Task<IActionResult> Authors()
    {
        var authors = await _authorService.GetAllAsync();
        return View(authors);
    }

    [HttpGet]
    public IActionResult CreateAuthor()
    {
        return View(new AuthorViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> CreateAuthor(AuthorViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        await _authorService.CreateAsync(model);
        return RedirectToAction(nameof(Authors));
    }

    [HttpGet]
    public async Task<IActionResult> EditAuthor(int id)
    {
        var model = await _authorService.GetByIdAsync(id);
        if (model is null) return NotFound();

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> EditAuthor(AuthorViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        await _authorService.UpdateAsync(model);
        return RedirectToAction(nameof(Authors));
    }

    [HttpPost]
    public async Task<IActionResult> DeleteAuthor(int id)
    {
        await _authorService.DeleteAsync(id);
        return RedirectToAction(nameof(Authors));
    }
}