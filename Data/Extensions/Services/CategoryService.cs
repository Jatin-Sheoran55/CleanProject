

using Application.Interfaces.Categorys;
using Application.Interfaces.Services.Categorys;
using Domain.Entities;

namespace Infrastructure.Extensions.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _repository;

    public CategoryService(ICategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<Category> CreateAsync(Category category)
    {
        return await _repository.CreateAsync(category);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }

    public async Task<List<Category>> GetAllAsync()
    {
      return await  _repository.GetAllAsync();
    }

    public async Task<Category?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<Category?> UpdateAsync(int id, Category category)
    {
        category.Id = id;
        return await _repository.UpdateAsync(category);
    }
}
