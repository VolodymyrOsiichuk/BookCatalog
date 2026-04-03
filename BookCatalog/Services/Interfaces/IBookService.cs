using BookCatalog.ViewModels.Books;

namespace BookCatalog.Services.Interfaces;

public interface IBookService
{
    Task<List<BookListItemViewModel>> GetAllAsync();
    Task<BookDetailsViewModel?> GetByIdAsync(int id);

    Task<CreateBookViewModel> GetCreateViewModelAsync();
    Task<EditBookViewModel?> GetEditViewModelAsync(int id);

    Task CreateAsync(CreateBookViewModel model);
    Task UpdateAsync(EditBookViewModel model);
    Task DeleteAsync(int id);
}