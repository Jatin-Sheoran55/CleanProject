

using Application.Dto.Orders;
using Application.Interfaces.Repositories.Carts;
using Application.Interfaces.Repositories.Orders;
using Application.Interfaces.Services.Orders;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Extensions.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepo;
    private readonly ICartRepository _cartRepo;

    public OrderService(IOrderRepository orderRepo, ICartRepository cartRepo)
    {
        _orderRepo = orderRepo;
        _cartRepo = cartRepo;
    }

    public async Task<OrderResponseDto> PlaceOrderAsync(int userId, string address)
    {
        var cart = await _cartRepo.GetCartByUserIdAsync(userId);

        if (cart == null || !cart.CartItems.Any())
            throw new Exception("Cart is empty. Please add items first.");

        var order = new Order
        {
            UserId = userId,
            DeliveryAddress = address,
            Items = cart.CartItems.Select(ci => new OrderItem
            {
                ProductId = ci.ProductId,
                Quantity = ci.Quantity,
                Price = ci.Product.Price
            }).ToList()
        };

        order.TotalAmount = order.Items.Sum(x => x.Price * x.Quantity);

        await _orderRepo.CreateAsync(order);

        // ✅ PERFECT
        await _cartRepo.ClearCartAsync(cart);

        return Map(order);
    }


    public async Task CancelOrderAsync(int orderId)
    {
        await UpdateStatusAsync(orderId, "Cancelled");
    }

    public async Task<List<OrderResponseDto>> GetUserOrdersAsync(int userId)
    {
        var orders = await _orderRepo.GetByUserIdAsync(userId);
        return orders.Select(Map).ToList();
    }

  

    public async Task UpdateStatusAsync(int orderId, string status)
    {
        var order = await _orderRepo.GetByIdAsync(orderId)
           ?? throw new Exception("Order not found");

        order.Status = status;
        await _orderRepo.UpdateAsync(order);
    }
   
    private OrderResponseDto Map(Order order)
    {
        return new OrderResponseDto
        {
            OrderId = order.Id,
            OrderDate = order.OrderDate,
            Status = order.Status,
            DeliveryAddress = order.DeliveryAddress,
            TotalAmount = order.TotalAmount,
            Items = order.Items.Select(i => new OrderItemResponseDto
            {
                ProductId = i.ProductId,
                ProductName = i.Product.Name,
                Quantity = i.Quantity,
                Price = i.Price
            }).ToList()
        };
    }
}

