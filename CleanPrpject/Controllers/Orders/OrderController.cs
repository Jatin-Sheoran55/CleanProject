using System.Security.Claims;
using Application.Dto.Orders;
using Application.Interfaces.Services.Orders;
using Infrastructure.Extensions.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace CleanProject.Controllers.Orders
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _service;

        public OrderController(IOrderService service)
        {
            _service = service;
        }







      

[Authorize]
    [HttpPost("place")]
    public async Task<IActionResult> PlaceOrder(CreateOrderDto dto)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var result = await _service.PlaceOrderAsync(userId, dto.TableNo);
        return Ok(result);
    }




    [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserOrders(int userId)
        {
            return Ok(await _service.GetUserOrdersAsync(userId));
        }

       
        [HttpPut("{orderId}/status")]
        public async Task<IActionResult> UpdateStatus(int orderId, UpdateOrderStatusDto dto)
        {
            await _service.UpdateStatusAsync(orderId, dto.Status);
            return Ok("Status updated");
        }

        [HttpPut("{orderId}/cancel")]
        public async Task<IActionResult> Cancel(int orderId)
        {
            await _service.CancelOrderAsync(orderId);
            return Ok("Order cancelled");
        }
    }
}
