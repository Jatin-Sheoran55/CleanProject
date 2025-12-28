

using Application.Dto.User;
using Domain.Entities;

namespace Application.Interfaces.Services.Users;

public interface IUserService
{
    Task<User?> GetByIdAsync(int id);
    Task<User?> UpdateAsync(int id, UpdateUserDto dto);
    Task<bool> DeleteAsync(int id);
}
