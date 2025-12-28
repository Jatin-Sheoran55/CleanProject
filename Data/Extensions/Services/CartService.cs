

using Application.Dto.AddToCarts;
using Application.Dto.UpdateCartItems;
using Application.Interfaces.Repositories.Carts;
using Application.Interfaces.Repositories.Products;
using Application.Interfaces.Services.Carts;
using Domain.Entities;

namespace Infrastructure.Extensions.Services;

public class CartService : ICartService
{
    private readonly ICartRepository _cartRepo;
    private readonly IProductRepository _productRepo;

    public CartService(ICartRepository cartRepo, IProductRepository productRepo)
    {
        _cartRepo = cartRepo;
        _productRepo = productRepo;
    }

    public async Task<CartResponseDto> GetCartAsync(int userId)
    {
        var cart = await _cartRepo.GetCartByUserIdAsync(userId)
                   ?? await _cartRepo.CreateCartAsync(userId);

        return MapToResponse(cart);
    }

    public async Task<CartResponseDto> AddToCartAsync(AddToCartDto dto)
    {
        var cart = await _cartRepo.GetCartByUserIdAsync(dto.UserId)
                   ?? await _cartRepo.CreateCartAsync(dto.UserId);

        var product = await _productRepo.GetByIdAsync(dto.ProductId);
        if (product == null) throw new Exception("Product not found");

        var existingItem = cart.CartItems
            .FirstOrDefault(x => x.ProductId == dto.ProductId);

        if (existingItem != null)
        {
            existingItem.Quantity += dto.Quantity;
            await _cartRepo.SaveAsync();
        }
        else
        {
            await _cartRepo.AddItemAsync(new UserCartItem
            {
                CartId = cart.Id,
                ProductId = dto.ProductId,
                Quantity = dto.Quantity
            });
        }

        cart = await _cartRepo.GetCartByUserIdAsync(dto.UserId);
        return MapToResponse(cart);
    }

    public async Task UpdateQuantityAsync(int cartItemId, UpdateCartItemDto dto)
    {
        var item = await _cartRepo.GetCartItemAsync(cartItemId);
        if (item == null) throw new Exception("Cart item not found");

        item.Quantity = dto.Quantity;
        await _cartRepo.SaveAsync();
    }

    public async Task RemoveItemAsync(int cartItemId)
    {
        var item = await _cartRepo.GetCartItemAsync(cartItemId);
        if (item == null) throw new Exception("Cart item not found");

        await _cartRepo.RemoveItemAsync(item);
    }

    private CartResponseDto MapToResponse(UserCart cart)
    {
        return new CartResponseDto
        {
            CartId = cart.Id,
            UserId = cart.UserId,
            Items = cart.CartItems.Select(ci => new CartItemResponseDto
            {
                CartItemId = ci.Id,
                ProductId = ci.ProductId,
                ProductName = ci.Product.Name,
                Price = ci.Product.Price,
                Quantity = ci.Quantity
            }).ToList()
        };
    }
}
