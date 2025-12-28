using Application.Dto.Image;
using Application.Dto.Product;
using Application.Interfaces.Services.Products;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanProject.Controllers.Products
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }


       

       
        [HttpPost]
        public async Task<IActionResult> Create(
    [FromBody] CreateProductDto dto)
        {
            var product = new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                CategoryId = dto.CategoryId
            };

            var result = await _service.CreateAsync(product);
            return Ok(result);
        }

      
        [HttpPost("create-with-image")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CreateWithImage(
    [FromForm] CreateProductWithImageDto dto)
        {
            string imagePath = string.Empty;

            if (dto.Image != null)
            {
                var uploadsFolder = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot/images"
                );

                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                var fileName = Guid.NewGuid() + Path.GetExtension(dto.Image.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                using var stream = new FileStream(filePath, FileMode.Create);
                await dto.Image.CopyToAsync(stream);

                imagePath = "/images/" + fileName;
            }

            var product = new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                CategoryId = dto.CategoryId,
                ImageUrl = imagePath
            };

            var result = await _service.CreateAsync(product);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _service.GetByIdAsync(id);
            if (product == null)
                return NotFound();

            return Ok(product);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Product product)
        {
            var result = await _service.UpdateAsync(id, product);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success)
                return NotFound();

            return Ok("Deleted Successfully");
        }
    }
}
