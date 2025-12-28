

using Application.Interfaces.Categorys;
using Domain.Entities;
using Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Extensions.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly ProjectContext _context;

    public CategoryRepository(ProjectContext context)
    {
        _context = context;
    }

    public async Task<Category> CreateAsync(Category category)
    {
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
        return category;

    }

    public async Task<bool> DeleteAsync(int id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category == null)
            return false;

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
        return true;

    }

    public async Task<List<Category>> GetAllAsync()
    {
        return await _context.Categories.ToListAsync();
    }

    public async Task<Category?> GetByIdAsync(int id)
    {
        return await _context.Categories.FindAsync(id);
    }

    public async Task<Category?> UpdateAsync(Category category)
    {
        var existing = await _context.Categories.FindAsync(category.Id);
        if (existing == null)
            return null;

        existing.Name = category.Name;
        existing.Description = category.Description;
        existing.ImageUrl = category.ImageUrl;

        await _context.SaveChangesAsync();
        return existing;
    }
}
