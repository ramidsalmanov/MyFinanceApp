using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyFinance.Core.Dto;
using MyFinance.Core.Services;

namespace MyFinance.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : BaseController
{
    private ICategoryService _categoryService;
    private IMapper _mapper;

    public CategoryController(IMapper mapper, ICategoryService categoryService)
    {
        _mapper = mapper;
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<IEnumerable<CategoryDto>> Get()
    {
        var entities = _categoryService.GetCategories();
        var categories = entities.ProjectTo<CategoryDto>(_mapper.ConfigurationProvider);
        return await categories.ToListAsync();
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task Delete(long id)
    {
        await _categoryService.DeleteAsync(id);
    }

    [HttpPost]
    public async Task<ActionResult<CategoryDto>> Post(CategoryDto dto)
    {
        var path = await _categoryService.CreateCategory(dto);
        return path;
    }
    //
    // protected override async Task OnUpdateAsync(long id, JsonPatchDocument<CategoryDto> document)
    // {
    //     var category = await _categoryService.GetByIdAsync(id);
    //
    //     if (category == null)
    //     {
    //         throw new Exception();
    //     }
    //
    //     Apply(category, document);
    //
    //     await _categoryService.UpdateAsync(category);
    // }
}