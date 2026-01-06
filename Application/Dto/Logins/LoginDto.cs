
using System.ComponentModel.DataAnnotations;

namespace Application.Dto.Logins;

public class LoginDto
{
    [Required]
    [EmailAddress(ErrorMessage = "Please enter a valid email address")]
    public string Email { get; set; }
    public string Password { get; set; }
}
