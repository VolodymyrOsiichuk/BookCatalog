using BookCatalog.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookCatalog.Controllers;

public class AuthorsController : Controller
{
    private readonly IAuthorService _authorService;

    public AuthorsController(IAuthorService authorService)
    {
        _authorService = authorService;
    }

    public async Task<IActionResult> Index()
    {
        var authors = await _authorService.GetAllAsync();
        return View(authors);
    }
}