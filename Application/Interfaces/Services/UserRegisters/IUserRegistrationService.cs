

using Application.Dto.UserAdress;
using Domain.Entities;

namespace Application.Interfaces.Services.UserRegisters;

public interface IUserRegistrationService
{
    Task<User> RegisterAsync(RegisterUserWithAddressDto dto);
}
