

using Application.Dto.Adress;
using Domain.Entities;

namespace Application.Interfaces.Services.Addresss;

public interface IAddressService
{
    Task<Address?> UpdateAsync(int id, UpdateAddressDto dto);
    Task<bool> DeleteAsync(int id);
}
