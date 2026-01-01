

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Dto.Logins;
using Application.Interfaces.Repositories.Auths;
using Application.Interfaces.Repositories.Users;
using Application.Interfaces.Services.Auths;
using Application.Interfaces.Services.Emails;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Extensions.Services;

public class AuthService : IAuthService
{

    private readonly IUserRepository _userRepo;
    private readonly IAuthRepository _authRepo;
    private readonly IEmailService _emailService;
    private readonly IPasswordHasher<User> _hasher;
    private readonly IConfiguration _config;

    public AuthService(
        IUserRepository userRepo,
        IAuthRepository authRepo,
        IEmailService emailService,
        IPasswordHasher<User> hasher,
        IConfiguration config)
    {
        _userRepo = userRepo;
        _authRepo = authRepo;
        _emailService = emailService;
        _hasher = hasher;
        _config = config;
    }
    public async Task ChangePasswordAsync(int userId, ChangePasswordDto dto)
    {
        var user = await _userRepo.GetByIdAsync(userId)
          ?? throw new Exception("User not found");

        var result = _hasher.VerifyHashedPassword(user, user.PasswordHash, dto.OldPassword);
        if (result == PasswordVerificationResult.Failed)
            throw new Exception("Wrong password");

        user.PasswordHash = _hasher.HashPassword(user, dto.NewPassword);
        await _userRepo.UpdateAsync(user);
    }

    public async Task<bool> ForgotPasswordAsync(ForgotPasswordDto dto)
    {
        var user = await _userRepo.GetByEmailAsync(dto.Email);
        if (user == null)
            return false;

        var otp = new Random().Next(100000, 999999).ToString();

        await _authRepo.CreateOtpAsync(new UserOtp
        {
            UserId = user.Id,
            Otp = otp,
            ExpiryTime = DateTime.UtcNow.AddMinutes(10)
        });

        await _emailService.SendEmailAsync(
            user.Email,
            "OTP",
            $"Your OTP is {otp}"
        );

        return true;
    }

    public async Task<LoginResponseDto> LoginAsync(LoginDto dto)
    {
        var user = await _userRepo.GetByEmailAsync(dto.Email);
        if (user == null)
            return null;

        var result = _hasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
        if (result == PasswordVerificationResult.Failed)
            return null;

        var claims = new[]
        {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Role, user.Role),
        new Claim(ClaimTypes.Email, user.Email)
    };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

        var token = new JwtSecurityToken(
     issuer: _config["Jwt:Issuer"],
     audience: _config["Jwt:Audience"],
     claims: claims,
     expires: DateTime.UtcNow.AddMinutes(
         int.Parse(_config["Jwt:ExpiryInMinutes"])
     ),
     signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
 );
        return new LoginResponseDto
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            Role = user.Role
        };
    }

    public async Task ResetPasswordAsync(ResetPasswordDto dto)
    {
        var user = await _userRepo.GetByEmailAsync(dto.Email)
           ?? throw new Exception("User not found");

        user.PasswordHash = _hasher.HashPassword(user, dto.NewPassword);
        await _userRepo.UpdateAsync(user);
    }

    public async Task VerifyOtpAsync(VerifyOtpDto dto)
    {
        var user = await _userRepo.GetByEmailAsync(dto.Email)
          ?? throw new Exception("User not found");

        var otp = await _authRepo.GetValidOtpAsync(user.Id, dto.Otp)
            ?? throw new Exception("Invalid OTP");

        otp.IsUsed = true;
        await _authRepo.SaveAsync();
    }
}
