using MyFinance.Common.Models;
using MyFinance.Core.Dto;
using MyFinance.Domain.Models;

namespace MyFinance.Core.Services;

public interface ICategoryService
{
    IQueryable<Category> GetCategories();
    Task<CategoryDto> CreateCategory(CategoryDto categoryDto);
    Task<ActionResult> DeleteAsync(long id);
}