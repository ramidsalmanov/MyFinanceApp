using System.Security.Claims;

namespace MyFinance.Core.Identity;

public interface ITokenService
{
    string CreateToken(IEnumerable<Claim> claims);
    string CreateRefreshToken();
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}