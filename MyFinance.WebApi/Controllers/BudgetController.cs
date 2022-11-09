using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyFinance.Core.Dto.Budget;
using MyFinance.Core.Services;
using MyFinance.Domain.Models.Budget;
using MyFinance.Persistence;
using ActionResult = MyFinance.Common.Models.ActionResult;

namespace MyFinance.WebApi.Controllers;

[ApiController]
[Route("api/[controller]s")]
public class BudgetController : BaseController
{
    private IDbContext _context;
    private IUserService _userService;
    private IBudgetService _budgetService;

    public BudgetController(IMapper mapper, IDbContext context, IUserService userService, IBudgetService budgetService)
        : base(mapper)
    {
        _context = context;
        _userService = userService;
        _budgetService = budgetService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BudgetDto>>> Get()
    {
        var u = _userService.CurrentUser.Id;
        var budgets = _context.Set<Budget>().Where(x => x.AppUserId == u);
        return await budgets.ProjectTo<BudgetDto>(Mapper.ConfigurationProvider).ToListAsync();
    }

    [HttpGet("{budgetId}")]
    public ActionResult<IEnumerable<BudgetItemDto>> GetBudgetItems(long budgetId)
    {
        var budgetItems = _context.Set<BudgetItem>().Where(x => x.BudgetId == budgetId);
        var dto = budgetItems.ProjectTo<BudgetItemDto>(Mapper.ConfigurationProvider);
        return Ok(dto);
    }

    [HttpPost]
    public async Task<IActionResult> Post(BudgetDto budgetDto)
    {
        var budgetItems = Mapper.Map<IEnumerable<BudgetItem>>(budgetDto.BudgetItems);
        await _budgetService.AddOrUpdateAsync(budgetDto.Id, budgetItems);
        return Ok();
    }
}