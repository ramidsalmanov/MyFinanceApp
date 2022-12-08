using MyFinance.Domain.Models;

namespace MyFinance.Core.Dto;

public class CategoryDto : BaseDto
{
    public string Name { get; set; }
    public string Path { get; set; }
    public CategoryType Type { get; set; }

    public IEnumerable<CategoryDto> Children { get; set; }
}