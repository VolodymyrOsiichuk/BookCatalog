using BookCatalog.ViewModels.Authors;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookCatalog.Services.Interfaces;

public interface IAuthorService
{
    Task<List<AuthorViewModel>> GetAllAsync();
    Task<List<SelectListItem>> GetSelectListAsync();

    Task<AuthorViewModel?> GetByIdAsync(int id);
    Task CreateAsync(AuthorViewModel model);
    Task UpdateAsync(AuthorViewModel model);
    Task DeleteAsync(int id);
}