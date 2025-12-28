

using Application.Interfaces.Repositories.Products;
using Application.Interfaces.Services.Products;
using Domain.Entities;

namespace Infrastructure.Extensions.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }
    public async Task<Product> CreateAsync(Product product)
    {
        return await _repository.CreateAsync(product);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }

    public async Task<List<Product>> GetAllAsync()
    {

        return await _repository.GetAllAsync();
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<Product?> UpdateAsync(int id, Product product)
    {

        product.Id = id;
        return await _repository.UpdateAsync(product);
    }
}
