

using Microsoft.AspNetCore.Http;

namespace Application.Dto.Category;

public class CreateCategoryDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public IFormFile? Image { get; set; }
}
