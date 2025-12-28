

using Application.Interfaces.Repositories.Users;
using Domain.Entities;
using Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Extensions.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ProjectContext _context;

    public UserRepository(ProjectContext context)
    {
        _context = context;
    }
    public async Task<User> CreateAsync(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return false;

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return await _context.Users
          .Include(u => u.Addresses)
          .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<User?> UpdateAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        return user;
    }
}
