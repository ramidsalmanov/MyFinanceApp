using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyFinance.Common.Models;
using MyFinance.Core.Dto;
using MyFinance.Core.Services;
using MyFinance.Domain.Models;
using MyFinance.Domain.Models.Identity;
using MyFinance.Persistence;

namespace MyFinance.WebApi.Services;

internal class CategoryService : ICategoryService
{
    private IDbContext _context;
    private IHttpContextAccessor _httpContextAccessor;
    private IUserService _userService;
    private IMapper _mapper;

    public CategoryService(IDbContext context, IHttpContextAccessor httpContextAccessor, IUserService userService,
        IMapper mapper)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
        _userService = userService;
        _mapper = mapper;
    }

    public IQueryable<Category> GetCategories()
    {
        var user = _userService.CurrentUser;
        var categories = _context.Set<AppUser>()
            .Include(x => x.Categories).SelectMany(x => x.Categories)
            .Where(x => x.Users.Contains(user));
        //Checker.NotNull(appUser, new UserNotFoundException());
        return categories;
    }

    public Task CreateCategory(Category categoryDto)
    {
        throw new System.NotImplementedException();
    }

    public async Task<CategoryDto> CreateCategory(CategoryDto categoryDto)
    {
        var user = _userService.CurrentUser;
        var category = _mapper.Map<Category>(categoryDto);
        user.Categories.Add(category);
        await _userService.UpdateAsync(user);
        return categoryDto;
    }

    public async Task<ActionResult> DeleteAsync(long id)
    {
        var category = await _context.Set<Category>().FindAsync(id);
        if (category == null)
        {
            return ActionResult.Fail($"category with id {id} not found");
        }

        var categories = _context.Set<Category>()
            .Where(x => ((LTree)x.Path).IsDescendantOf(category.Path));

        _context.Set<Category>().RemoveRange(categories);
        await _context.SaveChangesAsync();
        return ActionResult.Ok();
    }
}