using Application.Interfaces.Repositories.Products;
using Domain.Entities;
using Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Extensions.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ProjectContext _context;

    public ProductRepository(ProjectContext context)
    {
        _context = context;
    }
    public async Task<Product> CreateAsync(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
            return false;

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<Product>> GetAllAsync()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        return await _context.Products.FindAsync(id);
    }

    public async Task<Product?> UpdateAsync(Product product)
    {
        var existing = await _context.Products.FindAsync(product.Id);
        if (existing == null)
            return null;

        existing.Name = product.Name;
        existing.Price = product.Price;
        

        await _context.SaveChangesAsync();
        return existing;
    }
}
