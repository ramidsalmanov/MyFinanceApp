using MyFinance.Common.Models;
using MyFinance.Domain.Models;

namespace MyFinance.Core.Services;

public interface IAccountService
{
    Task<IQueryable<Account>> GetUserAccountsAsync();
    Task<Account> CreateAsync(Account account);
    Task<ActionResult> DeleteAsync(long id);
    Task<Account> GetAccountAsync(long id);
    Task UpdateAsync(Account account);
}