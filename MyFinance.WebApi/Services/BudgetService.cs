using MyFinance.Common;
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
        budget.BudgetItems = budgetItems.ToList();

        await _context.SaveChangesAsync();
    }
}