using BookCatalog.ViewModels.Genres;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookCatalog.Services.Interfaces;

public interface IGenreService
{
    Task<List<GenreViewModel>> GetAllAsync();
    Task<List<SelectListItem>> GetSelectListAsync();

    Task<GenreViewModel?> GetByIdAsync(int id);
    Task CreateAsync(GenreViewModel model);
    Task UpdateAsync(GenreViewModel model);
    Task DeleteAsync(int id);
}