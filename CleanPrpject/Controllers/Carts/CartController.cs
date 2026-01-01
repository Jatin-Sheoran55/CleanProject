using System.Security.Claims;
using Application.Dto.AddToCarts;
using Application.Dto.UpdateCartItems;
using Application.Interfaces.Services.Carts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanProject.Controllers.Carts
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [Authorize]
        [HttpPost("add")]
        public async Task<IActionResult> AddToCart(AddToCartDto dto)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            return Ok(await _cartService.AddToCartAsync(userId, dto));
        }


        //[HttpGet("user/{userId}")]
        //public async Task<IActionResult> GetCart(int userId)
        //{
        //    return Ok(await _cartService.GetCartAsync(userId));
        //}

        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> GetMyCart()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            return Ok(await _cartService.GetCartAsync(userId));
        }




        [HttpPut("item/{id}")]
        public async Task<IActionResult> UpdateQuantity(int id, UpdateCartItemDto dto)
        {
            await _cartService.UpdateQuantityAsync(id, dto);
            return Ok();
        }

        [HttpDelete("item/{id}")]
        public async Task<IActionResult> RemoveItem(int id)
        {
            await _cartService.RemoveItemAsync(id);
            return Ok();
        }
    }
}
