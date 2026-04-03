using BookCatalog.Data;
using BookCatalog.Models.Entitites;
using BookCatalog.Services.Interfaces;
using BookCatalog.ViewModels.Authors;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BookCatalog.Services;

public class AuthorService : IAuthorService
{
    private readonly AppDbContext _context;

    public AuthorService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<AuthorViewModel>> GetAllAsync()
    {
        return await _context.Authors
            .AsNoTracking()
            .OrderBy(x => x.FullName)
            .Select(x => new AuthorViewModel
            {
                Id = x.Id,
                FullName = x.FullName,
                Biography = x.Biography,
                Email = x.Email
            })
            .ToListAsync();
    }

    public async Task<List<SelectListItem>> GetSelectListAsync()
    {
        return await _context.Authors
            .AsNoTracking()
            .OrderBy(x => x.FullName)
            .Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.FullName
            })
            .ToListAsync();
    }

    public async Task<AuthorViewModel?> GetByIdAsync(int id)
    {
        return await _context.Authors
            .AsNoTracking()
            .Where(x => x.Id == id)
            .Select(x => new AuthorViewModel
            {
                Id = x.Id,
                FullName = x.FullName,
                Biography = x.Biography,
                Email = x.Email
            })
            .FirstOrDefaultAsync();
    }

    public async Task CreateAsync(AuthorViewModel model)
    {
        var author = new Author
        {
            FullName = model.FullName,
            Biography = model.Biography,
            Email = model.Email
        };

        _context.Authors.Add(author);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(AuthorViewModel model)
    {
        var author = await _context.Authors.FirstOrDefaultAsync(x => x.Id == model.Id);
        if (author is null) return;

        author.FullName = model.FullName;
        author.Biography = model.Biography;
        author.Email = model.Email;

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var author = await _context.Authors.FirstOrDefaultAsync(x => x.Id == id);
        if (author is null) return;

        _context.Authors.Remove(author);
        await _context.SaveChangesAsync();
    }
}