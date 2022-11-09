using MyFinance.Common.Models;
using MyFinance.Domain.Models;
using MyFinance.Domain.Models.Identity;

namespace MyFinance.Core.Services;

public interface IUserService
{
    AppUser CurrentUser { get; }
    Task<ActionResult> CreateUser(string name, string password);
    Task<AppUser> FindByNameAsync(string name);
    Task<bool> CheckPasswordAsync(AppUser user, string password);
    Task<ActionResult> UpdateAsync(AppUser user);
    Task<string> GetUserIdAsync();
}