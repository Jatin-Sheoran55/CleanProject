
using Domain.Entities;

namespace Application.Interfaces.Repositories.Users;

public interface IUserRepository
{
    Task<User> CreateAsync(User user);
    Task<User?> GetByIdAsync(int id);
    Task<User?> GetByEmailAsync(string email);
    Task<User?> UpdateAsync(User user);
    Task<bool> DeleteAsync(int id);
}
