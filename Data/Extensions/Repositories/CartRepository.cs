

using Application.Interfaces.Repositories.Carts;
using Domain.Entities;
using Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Extensions.Repositories;

public class CartRepository : ICartRepository
{
    private readonly ProjectContext _context;

    public CartRepository(ProjectContext context)
    {
        _context = context;
    }

    public async Task<UserCart> GetCartByUserIdAsync(int userId)
    {
        return await _context.UserCarts
            .Include(c => c.CartItems)
            .ThenInclude(ci => ci.Product)
            .FirstOrDefaultAsync(c => c.UserId == userId);
    }

    public async Task<UserCart> CreateCartAsync(int userId)
    {
        var cart = new UserCart { UserId = userId };
        _context.UserCarts.Add(cart);
        await _context.SaveChangesAsync();
        return cart;
    }

    public async Task AddItemAsync(UserCartItem item)
    {
        _context.UserCartItems.Add(item);
        await _context.SaveChangesAsync();
    }

    public async Task<UserCartItem> GetCartItemAsync(int cartItemId)
    {
        return await _context.UserCartItems
            .Include(x => x.Product)
            .FirstOrDefaultAsync(x => x.Id == cartItemId);
    }

    public async Task RemoveItemAsync(UserCartItem item)
    {
        _context.UserCartItems.Remove(item);
        await _context.SaveChangesAsync();
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}
