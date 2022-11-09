using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyFinance.Core.Dto;
using MyFinance.Core.Services;
using MyFinance.WebApi.Helpers;

namespace MyFinance.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : BaseController
{
    private IUserService _userService;
    private ICategoryService _categoryService;
    private IValidator<CategoryDto> _validator;

    public CategoryController(IUserService userService, IMapper mapper, ICategoryService categoryService,
        IValidator<CategoryDto> validator) : base(mapper)
    {
        _userService = userService;
        _categoryService = categoryService;
        _validator = validator;
    }

    [HttpGet]
    public async Task<IEnumerable<CategoryDto>> Get()
    {
        var entities = _categoryService.GetCategories();
        var categories = entities.ProjectTo<CategoryDto>(Mapper.ConfigurationProvider);
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