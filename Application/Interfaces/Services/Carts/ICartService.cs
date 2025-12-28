

using Application.Dto.AddToCarts;
using Application.Dto.UpdateCartItems;

namespace Application.Interfaces.Services.Carts;

public interface ICartService
{
    Task<CartResponseDto> GetCartAsync(int userId);
    Task<CartResponseDto> AddToCartAsync(AddToCartDto dto);
    Task UpdateQuantityAsync(int cartItemId, UpdateCartItemDto dto);
    Task RemoveItemAsync(int cartItemId);
}
