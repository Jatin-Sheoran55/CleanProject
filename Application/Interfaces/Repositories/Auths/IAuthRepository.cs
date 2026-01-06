

using Domain.Entities;

namespace Application.Interfaces.Repositories.Auths;

public interface IAuthRepository
{
    Task<UserOtp> CreateOtpAsync(UserOtp otp);
    Task<UserOtp?> GetValidOtpAsync(int userId, int otp);
    Task SaveAsync();
}
