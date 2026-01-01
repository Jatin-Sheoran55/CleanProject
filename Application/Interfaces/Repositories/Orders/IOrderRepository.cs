
using Domain.Entities;

namespace Application.Interfaces.Repositories.Orders;

public interface IOrderRepository
{
    Task<Order> CreateAsync(Order order);
    Task<Order?> GetByIdAsync(int id);
    Task<List<Order>> GetByUserIdAsync(int userId);
    Task UpdateAsync(Order order);
}
