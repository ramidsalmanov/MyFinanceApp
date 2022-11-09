using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyFinance.Core.Dto.Identity;
using MyFinance.Core.Identity;
using MyFinance.Domain.Models.Identity;
using MyFinance.Persistence;

namespace MyFinance.WebApi.Controllers.Authenticate;

[Route("api/[controller]")]
[ApiController]
[AllowAnonymous]
public class TokenController : BaseController
{
    private ITokenService _tokenService;
    private IDbContext _dbContext;

    public TokenController(ITokenService tokenService, IDbContext context, IMapper mapper) : base(mapper)
    {
        _tokenService = tokenService;
        _dbContext = context;
    }

    [HttpPost]
    [Route("refresh")]
    public async Task<IActionResult> RefreshAccessToken(TokenDto tokenDto)
    {
        if (tokenDto is null || string.IsNullOrEmpty(tokenDto.AccessToken) ||
            string.IsNullOrEmpty(tokenDto.RefreshToken))
        {
            return BadRequest("Invalid client request");
        }

        var accessToken = tokenDto.AccessToken;
        var refreshToken = tokenDto.RefreshToken;
        try
        {
            var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);
            if (principal == null)
            {
                return BadRequest("Invalid refresh token");
            }

            var username = principal?.Identity?.Name; //this is mapped to the Name claim by default
            var user = await _dbContext.Set<AppUser>().SingleOrDefaultAsync(u => u.UserName == username);
            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                return BadRequest("Invalid client request");
            }

            var newAccessToken = _tokenService.CreateToken(principal.Claims);
            await _dbContext.SaveChangesAsync();
            return Ok(new TokenDto()
            {
                AccessToken = newAccessToken,
            });
        }
        catch (Exception e)
        {
            return Problem("Errorrrrrrrr");
        }
    }

    [HttpPost, Authorize]
    [Route("revoke")]
    public async Task<IActionResult> Revoke()
    {
        var username = User?.Identity?.Name;
        var user = await _dbContext.Set<AppUser>().SingleOrDefaultAsync(u => u.UserName == username);
        if (user == null) return BadRequest();
        user.RefreshToken = null;
        await _dbContext.SaveChangesAsync();
        return NoContent();
    }
}