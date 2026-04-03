using BookCatalog.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookCatalog.Controllers;


public class BooksController : Controller
{
    private readonly IBookService _bookService;

    public BooksController(
        IBookService bookService
    )
    {
        _bookService = bookService;
    }

    public async Task<IActionResult> Index()
    {
        var books = await _bookService.GetAllAsync();
        return View(books);
    }
    
    public async Task<IActionResult> Details(int id)
    {
        var book = await _bookService.GetByIdAsync(id);

        if (book is null)
        {
            return NotFound();
        }

        return View(book);
    }
}