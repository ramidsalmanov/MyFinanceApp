using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyFinance.Core.Dto.Identity;
using MyFinance.Core.Services;
using TokenDto = MyFinance.Core.Dto.Identity.TokenDto;

namespace MyFinance.WebApi.Controllers.Authenticate;

[Route("api/[controller]")]
[ApiController]
[AllowAnonymous]
public class AuthenticateController : BaseController
{
    private IUserService _userService;
    private IAuthenticationService _authenticationService;

    public AuthenticateController(IUserService userService, IAuthenticationService authenticationService)
    {
        _userService = userService;
        _authenticationService = authenticationService;
    }

    [HttpPost]
    [Route("login")]
    public async Task<ActionResult<TokenDto>> Login([FromBody] UserLoginDto loginDto)
    {
        var result = await _authenticationService.LoginAsync(loginDto.UserName, loginDto.Password);
        if (!result.Success)
        {
            return Unauthorized("Неверный логин или пароль");
        }

        return Ok(result.GetData<TokenDto>());
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register(UserRegisterDto registerDto)
    {
        var result = await _userService.CreateUser(registerDto.UserName, registerDto.Password);
        if (result.Failure)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error, error);
            }

            return Problem();
        }

        return Ok();
    }

    [HttpGet]
    [Route("logout")]
    public async Task<ActionResult> Logout()
    {
        await _authenticationService.LogoutAsync();
        return NoContent();
    }
}