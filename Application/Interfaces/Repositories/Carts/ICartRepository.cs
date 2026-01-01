

using Domain.Entities;

namespace Application.Interfaces.Repositories.Carts;

public interface ICartRepository
{
    Task<UserCart> GetCartByUserIdAsync(int userId);
    Task<UserCart> CreateCartAsync(int userId);
    Task AddItemAsync(UserCartItem item);
    Task<UserCartItem> GetCartItemAsync(int cartItemId);
    Task RemoveItemAsync(UserCartItem item);
    Task SaveAsync();
    Task ClearCartAsync(UserCart cart);
}
