

using Application.Dto.UserAdress;
using Application.Interfaces.Repositories.Addresss;
using Application.Interfaces.Repositories.Users;
using Application.Interfaces.Services.UserRegisters;
using Domain.Entities;
using Infrastructure.DataContext;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Extensions.Services;

public class UserRegistrationService : IUserRegistrationService
{
    private readonly IUserRepository _userRepo;
    private readonly IAddressRepository _addressRepo;
    private readonly ProjectContext _context;
    private readonly IPasswordHasher<User> _hasher;

    public UserRegistrationService(
        IUserRepository userRepo,
        IAddressRepository addressRepo,
        ProjectContext context,
        IPasswordHasher<User> passwordHasher)
    {
        _userRepo = userRepo;
        _addressRepo = addressRepo;
        _context = context;
        _hasher = passwordHasher;
    }

    public async Task<User> RegisterAsync(RegisterUserWithAddressDto dto)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            var user = new User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email
            };

            user.PasswordHash = _hasher.HashPassword(user, dto.Password);
            var createdUser = await _userRepo.CreateAsync(user);

            var address = new Address
            {
                UserId = createdUser.Id,
                Street = dto.Street,
                City = dto.City,
                State = dto.State,
                Country = dto.Country,
                Pincode = dto.Pincode
            };

            await _addressRepo.CreateAsync(address);

            await transaction.CommitAsync();
            return createdUser;
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}
