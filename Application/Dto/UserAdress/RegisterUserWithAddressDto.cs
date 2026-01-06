

using System.ComponentModel.DataAnnotations;

namespace Application.Dto.UserAdress;

public class RegisterUserWithAddressDto
{
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    [EmailAddress(ErrorMessage = "Email is not valid")]
    public string Email { get; set; }
    [Required]
    [MinLength(6)]
    public string Password { get; set; }

    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }
    public string Pincode { get; set; }
}
