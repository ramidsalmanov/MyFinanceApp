using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using MyFinance.Common.Models;
using MyFinance.Core.Dto.Identity;
using MyFinance.Core.Identity;
using MyFinance.Core.Services;
using MyFinance.Domain.Models.Identity;
using MyFinance.WebApi.Models;

namespace MyFinance.WebApi.Services;

public class AuthenticationService : IAuthenticationService
{
    private UserManager<AppUser> _userManager;
    private SignInManager<AppUser> _signInManager;
    private ITokenService _tokenService;
    private readonly JwtSettings _jwtSettings;

    public AuthenticationService(SignInManager<AppUser> signInManager,
        IOptions<JwtSettings> options,
        ITokenService tokenService, UserManager<AppUser> userManager)
    {
        _signInManager = signInManager;
        _tokenService = tokenService;
        _userManager = userManager;
        _jwtSettings = options.Value;
    }

    public async Task<ActionResult> LoginAsync(string name, string password)
    {
        var user = await _userManager.FindByNameAsync(name);
        if (user != null && await _userManager.CheckPasswordAsync(user, password))
        {
            var signInResult = await _signInManager.PasswordSignInAsync(user, password, true, true);
            if (!signInResult.Succeeded)
            {
                return ActionResult.Fail(signInResult.ToString());
            }

            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, name),
                new(ClaimTypes.NameIdentifier, user.Id),
            };
            
            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var token = _tokenService.CreateToken(claims);
            var refreshToken = _tokenService.CreateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpiryTime);
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return ActionResult.Ok(new TokenDto()
                {
                    AccessToken = token,
                    RefreshToken = refreshToken
                });
            }

            return ActionResult.Fail(result.Errors.Select(r => r.Description).ToArray());
        }

        return ActionResult.Fail("Invalid user data");
    }

    public async Task LogoutAsync()
    {
        try
        {
            await _signInManager.SignOutAsync();
        }
        catch
        {
            // ignored
        }
    }
}