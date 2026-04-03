using BookCatalog.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookCatalog.Controllers;

public class GenresController : Controller
{
    private readonly IGenreService _genreService;

    public GenresController(IGenreService genreService)
    {
        _genreService = genreService;
    }

    public async Task<IActionResult> Index()
    {
        var genres = await _genreService.GetAllAsync();
        return View(genres);
    }
}