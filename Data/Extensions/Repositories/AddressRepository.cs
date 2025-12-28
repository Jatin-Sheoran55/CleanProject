
using Application.Interfaces.Repositories.Addresss;
using Domain.Entities;
using Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Extensions.Repositories;

public class AddressRepository : IAddressRepository
{
    private readonly ProjectContext _context;

    public AddressRepository(ProjectContext context)
    {
        _context = context;
    }
    public async Task<Address> CreateAsync(Address address)
    {
        _context.Addresses.Add(address);
        await _context.SaveChangesAsync();
        return address;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var address = await _context.Addresses.FindAsync(id);
        if (address == null) return false;

        _context.Addresses.Remove(address);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<Address?> GetByIdAsync(int id)
    {
        return await _context.Addresses
      .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<Address?> UpdateAsync(Address address)
    {
        _context.Addresses.Update(address);
        await _context.SaveChangesAsync();
        return address;
    }
}
