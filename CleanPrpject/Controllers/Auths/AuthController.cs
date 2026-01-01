using Application.Dto.Logins;
using Application.Interfaces.Services.Auths;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanProject.Controllers.Auths
{
   
   

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;

        public AuthController(IAuthService service)
        {
            _service = service;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var result = await _service.LoginAsync(dto);

            if (result == null)
                return Unauthorized("Invalid email or password");

            return Ok(result);
        }
        [HttpPost("forgot-password")]
        public async Task<IActionResult> Forgot(ForgotPasswordDto dto)
        {
            var success = await _service.ForgotPasswordAsync(dto);

            if (!success)
                return NotFound("User with this email does not exist");

            return Ok("OTP sent successfully");
        }

        [HttpPost("verify-otp")]
        public async Task<IActionResult> Verify(VerifyOtpDto dto)
        {
            await _service.VerifyOtpAsync(dto);
            return Ok("OTP verified");
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> Reset(ResetPasswordDto dto)
        {
            await _service.ResetPasswordAsync(dto);
            return Ok("Password reset");
        }

        [Authorize]
        [HttpPost("change-password")]
        public async Task<IActionResult> Change(ChangePasswordDto dto)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            await _service.ChangePasswordAsync(userId, dto);
            return Ok();
        }

        [Authorize]
        [HttpGet("me")]
        public IActionResult Me()
        {
            return Ok(new
            {
                Id = User.FindFirstValue(ClaimTypes.NameIdentifier),
                Email = User.FindFirstValue(ClaimTypes.Email),
                Role = User.FindFirstValue(ClaimTypes.Role)
            });
        }
    }
}
