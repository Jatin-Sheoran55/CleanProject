

using Application.Dto.Adress;
using Application.Interfaces.Repositories.Addresss;
using Application.Interfaces.Services.Addresss;
using Domain.Entities;

namespace Infrastructure.Extensions.Services;

public class AddressService : IAddressService
{
    private readonly IAddressRepository _addressRepo;

    public AddressService(IAddressRepository addressRepo)
    {
        _addressRepo = addressRepo;
    }
    public async Task<bool> DeleteAsync(int id)
    {
        return await _addressRepo.DeleteAsync(id);
    }

    public async Task<Address?> UpdateAsync(int id, UpdateAddressDto dto)
    {
        var address = await _addressRepo.GetByIdAsync(id);
        if (address == null) return null;

        address.Street = dto.Street;
        address.City = dto.City;
        address.State = dto.State;
        address.Country = dto.Country;
        address.Pincode = dto.Pincode;

        return await _addressRepo.UpdateAsync(address);
    }
}
