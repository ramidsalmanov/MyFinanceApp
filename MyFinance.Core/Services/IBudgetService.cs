using MyFinance.Domain.Models.Budget;

namespace MyFinance.Core.Services;

public interface IBudgetService
{
    Task AddOrUpdateAsync(long budgetId, IEnumerable<BudgetItem> budgetItems);
}