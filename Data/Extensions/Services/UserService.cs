

using Application.Dto.User;
using Application.Interfaces.Repositories.Users;
using Application.Interfaces.Services.Users;
using Domain.Entities;

namespace Infrastructure.Extensions.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepo;

    public UserService(IUserRepository userRepo)
    {
        _userRepo = userRepo;
    }
    public async Task<bool> DeleteAsync(int id)
    {
        return await _userRepo.DeleteAsync(id);
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return await _userRepo.GetByIdAsync(id);
    }

    public async Task<User?> UpdateAsync(int id, UpdateUserDto dto)
    {
        var user = await _userRepo.GetByIdAsync(id);
        if (user == null) return null;

        user.FirstName = dto.FirstName;
        user.LastName = dto.LastName;

        return await _userRepo.UpdateAsync(user);
    }
}
