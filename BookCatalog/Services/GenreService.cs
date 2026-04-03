using BookCatalog.Data;
using BookCatalog.Models.Entitites;
using BookCatalog.Services.Interfaces;
using BookCatalog.ViewModels.Genres;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BookCatalog.Services;

public class GenreService : IGenreService
{
    private readonly AppDbContext _context;

    public GenreService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<GenreViewModel>> GetAllAsync()
    {
        return await _context.Genres
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .Select(x => new GenreViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description
            })
            .ToListAsync();
    }

    public async Task<List<SelectListItem>> GetSelectListAsync()
    {
        return await _context.Genres
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            })
            .ToListAsync();
    }

    public async Task<GenreViewModel?> GetByIdAsync(int id)
    {
        return await _context.Genres
            .AsNoTracking()
            .Where(x => x.Id == id)
            .Select(x => new GenreViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description
            })
            .FirstOrDefaultAsync();
    }

    public async Task CreateAsync(GenreViewModel model)
    {
        var genre = new Genre
        {
            Name = model.Name,
            Description = model.Description
        };

        _context.Genres.Add(genre);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(GenreViewModel model)
    {
        var genre = await _context.Genres.FirstOrDefaultAsync(x => x.Id == model.Id);
        if (genre is null) return;

        genre.Name = model.Name;
        genre.Description = model.Description;

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var genre = await _context.Genres.FirstOrDefaultAsync(x => x.Id == id);
        if (genre is null) return;

        _context.Genres.Remove(genre);
        await _context.SaveChangesAsync();
    }
}