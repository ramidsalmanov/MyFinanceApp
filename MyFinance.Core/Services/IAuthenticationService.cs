using MyFinance.Common.Models;

namespace MyFinance.Core.Services;

public interface IAuthenticationService
{
    Task<ActionResult> LoginAsync(string name, string password);
    Task LogoutAsync();
}