

using Application.Interfaces.Repositories.Auths;
using Domain.Entities;
using Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Extensions.Repositories;

public class AuthRepository : IAuthRepository
{
    private readonly ProjectContext _context;

    public AuthRepository(ProjectContext context)
    {
        _context = context;
    }
    public async Task<UserOtp> CreateOtpAsync(UserOtp otp)
    {
        _context.UserOtps.Add(otp);
        await _context.SaveChangesAsync();
        return otp;
    }

    public async Task<UserOtp?> GetValidOtpAsync(int userId, string otp)
    {
        return await _context.UserOtps
          .FirstOrDefaultAsync(x =>
              x.UserId == userId &&
              x.Otp == otp &&
              x.ExpiryTime > DateTime.UtcNow &&
              !x.IsUsed);
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}
