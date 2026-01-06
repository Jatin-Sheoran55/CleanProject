

using System.ComponentModel.DataAnnotations;

namespace Application.Dto.Logins;

public class ForgotPasswordDto
{
    [Required]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string Email { get; set; }
}
