
using Domain.Entities;

namespace Application.Interfaces.Repositories.Addresss;

public interface IAddressRepository
{
    Task<Address> CreateAsync(Address address);
    Task<Address?> GetByIdAsync(int id);
    Task<Address?> UpdateAsync(Address address);
    Task<bool> DeleteAsync(int id);
}
