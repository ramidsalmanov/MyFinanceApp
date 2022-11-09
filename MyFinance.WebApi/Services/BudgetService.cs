using MyFinance.Common;
using MyFinance.Core.Dto.Budget;
using MyFinance.Core.Services;
using MyFinance.Domain.Models.Budget;
using MyFinance.Persistence;

namespace MyFinance.WebApi.Services;

public class BudgetService : IBudgetService
{
    private IDbContext _context;

    public BudgetService(IDbContext context)
    {
        _context = context;
    }

    public async Task AddOrUpdateAsync(long budgetId, IEnumerable<BudgetItem> budgetItems)
    {
        var budget = await _context.Set<Budget>().FindAsync(budgetId);
        Checker.NotNull(budget);
        foreach (var budgetItem in budgetItems)
        {
            budget.BudgetItems.Add(budgetItem);
        }

        await _context.SaveChangesAsync();
    }
}