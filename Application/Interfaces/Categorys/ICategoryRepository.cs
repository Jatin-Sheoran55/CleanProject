

using Domain.Entities;

namespace Application.Interfaces.Categorys;

public interface ICategoryRepository
{

    Task<List<Category>> GetAllAsync();
    Task<Category?> GetByIdAsync(int id);
    Task<Category> CreateAsync(Category category);
    Task<Category?> UpdateAsync(Category category);  
    Task<bool> DeleteAsync(int id);
}
