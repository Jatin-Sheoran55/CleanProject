using Application.Dto.Category;
using Application.Interfaces.Services.Categorys;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanProject.Controllers.Categorys
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _service;

        public CategoryController(ICategoryService service)
        {
            _service = service;
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] CreateCategoryDto dto)
        {
            string imagePath = string.Empty;

            if (dto.Image != null)
            {
                var uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/categories");
                if (!Directory.Exists(uploads))
                    Directory.CreateDirectory(uploads);

                var fileName = Guid.NewGuid() + Path.GetExtension(dto.Image.FileName);
                var filePath = Path.Combine(uploads, fileName);

                using var stream = new FileStream(filePath, FileMode.Create);
                await dto.Image.CopyToAsync(stream);

                imagePath = "/categories/" + fileName;
            }

            var category = new Category
            {
                Name = dto.Name,
                Description = dto.Description,
                ImageUrl = imagePath
            };

            var result = await _service.CreateAsync(category);
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
            var category = await _service.GetByIdAsync(id);
            if (category == null)
                return NotFound();

            return Ok(category);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Category category)
        {
            var result = await _service.UpdateAsync(id, category);
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

            return Ok("Category deleted successfully");
        }
    }
}
