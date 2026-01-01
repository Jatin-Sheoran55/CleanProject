
using Application.Dto.Orders;
using Domain.Entities;

namespace Application.Interfaces.Services.Orders;

public interface IOrderService
{
    Task<OrderResponseDto> PlaceOrderAsync(int userId, string address);
    Task<List<OrderResponseDto>> GetUserOrdersAsync(int userId);
    Task UpdateStatusAsync(int orderId, string status);
    Task CancelOrderAsync(int orderId);
   

}
