using Microsoft.AspNetCore.Identity;
using MyFinance.Common;
using MyFinance.Common.Models;
using MyFinance.Core.Exceptions;
using MyFinance.Core.Services;
using MyFinance.Domain.Models.Identity;
using MyFinance.Persistence;

namespace MyFinance.WebApi.Services;

public class UserService : IUserService
{
    private UserManager<AppUser> _userManager;
    private IDbContext _dbContext;
    private IHttpContextAccessor _httpContextAccessor;
    private AppUser _currentUser;

    public UserService(IDbContext dbContext,
        IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager)
    {
        _dbContext = dbContext;
        _httpContextAccessor = httpContextAccessor;
        _userManager = userManager;
    }

    public AppUser CurrentUser
    {
        get
        {
            if (_currentUser == null)
            {
                _currentUser = _userManager.GetUserAsync(_httpContextAccessor.HttpContext?.User).Result;
            }

            return _currentUser;
        }
    }

    public async Task<ActionResult> CreateUser(string name, string password)
    {
        if (name.IsNullOrEmpty() || password.IsNullOrEmpty())
        {
            return ActionResult.Fail("invalid user data");
        }

        if ((await _userManager.FindByNameAsync(name)) != null)
        {
            return ActionResult.Fail("user already exists");
        }

        var user = new AppUser()
        {
            UserName = name,
        };

        using var transaction = await _dbContext.Database.BeginTransactionAsync();

        try
        {
            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                return ActionResult.Fail(result.Errors.Select(e => e.Description).ToArray());
            }

            await transaction.CommitAsync();
            return ActionResult.Ok(user);
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            return ActionResult.Fail(e.Message);
        }
    }

    public async Task<AppUser> FindByNameAsync(string name)
    {
        return (await _userManager.FindByNameAsync(name));
    }

    public async Task<bool> CheckPasswordAsync(AppUser user, string password)
    {
        var identityUser = await _userManager.FindByIdAsync(user.Id);
        return await _userManager.CheckPasswordAsync(identityUser, password);
    }

    public async Task<ActionResult> UpdateAsync(AppUser user)
    {
        var identityUser = await _userManager.FindByIdAsync(user.Id);
        var updateResult = await _userManager.UpdateAsync(identityUser);
        if (updateResult.Succeeded)
        {
            return ActionResult.Ok();
        }

        return ActionResult.Fail(updateResult.Errors.Select(x => x.Description).ToArray());
    }

    public async Task<string> GetUserIdAsync()
    {
        var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext?.User);
        Checker.NotNull(user, new UserNotFoundException());
        return user.Id;
    }
}