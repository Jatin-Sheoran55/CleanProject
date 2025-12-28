
using Application.Dto.Address;

namespace Application.Dto.User;

public class UserResponseDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    public List<AddressResponseDto> Addresses { get; set; } = new();
}
