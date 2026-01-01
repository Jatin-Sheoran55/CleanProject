

using Application.Dto.Logins;

namespace Application.Interfaces.Services.Auths;

public interface IAuthService
{
    Task<LoginResponseDto> LoginAsync(LoginDto dto);
    Task<bool> ForgotPasswordAsync(ForgotPasswordDto dto);
    Task VerifyOtpAsync(VerifyOtpDto dto);
    Task ResetPasswordAsync(ResetPasswordDto dto);
    Task ChangePasswordAsync(int userId, ChangePasswordDto dto);
}
