using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyFinance.Common;
using MyFinance.Common.Models;
using MyFinance.Core.Exceptions;
using MyFinance.Core.Services;
using MyFinance.Domain.Models;
using MyFinance.Domain.Models.Identity;
using MyFinance.Persistence;

namespace MyFinance.WebApi.Services;

internal class AccountService : IAccountService
{
    private IHttpContextAccessor _httpContextAccessor;
    private IUserService _userService;
    private IDbContext _context;

    private DbSet<Account> AccountsSet => _context.Set<Account>();

    public AccountService(IHttpContextAccessor httpContextAccessor, IDbContext context, IUserService userService)
    {
        _httpContextAccessor = httpContextAccessor;
        _context = context;
        _userService = userService;
    }

    public async Task<IQueryable<Account>> GetUserAccountsAsync()
    {
        var userId = await _userService.GetUserIdAsync();
        return _context.Set<Account>().Where(x => x.AppUserId == userId);
    }

    public async Task<Account> CreateAsync(Account account)
    {
        Checker.NotNull(account);
        var userId = await _userService.GetUserIdAsync();
        account.AppUserId = userId;
        _context.Set<Account>().Add(account);
        await _context.SaveChangesAsync();
        return account;
    }

    public async Task<ActionResult> DeleteAsync(long id)
    {
        var userId = await _userService.GetUserIdAsync();
        var account = _context.Set<Account>().FirstOrDefault(x => x.Id == id && x.AppUserId == userId);
        if (account == null)
        {
            return ActionResult.Fail("account not found");
        }

        _context.Set<Account>().Remove(account);
        await _context.SaveChangesAsync();
        return ActionResult.Ok();
    }

    public async Task<Account> GetAccountAsync(long id)
    {
        Checker.IsTrue(id != default, "id cannot be default");
        return await AccountsSet.FindAsync(id);
    }

    public Task UpdateAsync(Account account)
    {
        Checker.NotNull(account);
        AccountsSet.Update(account);
        return _context.SaveChangesAsync();
    }

    // private async Task<AppUser> GetUserAsync()
    // {
    //     var user = await _userService.GetUserAsync(_httpContextAccessor.HttpContext?.User);
    //     if (user == null)
    //     {
    //         throw new UserNotFoundException();
    //     }
    //
    //     return user;
    // }
}