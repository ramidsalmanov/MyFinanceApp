using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyFinance.Core.Dto.Account;
using MyFinance.Core.Services;
using MyFinance.Domain.Models;

namespace MyFinance.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : BaseController
{
    private IUserService _userService;
    private IAccountService _accountService;

    public AccountController(IUserService userService, IMapper mapper, IAccountService accountService) : base(mapper)
    {
        _userService = userService;
        _accountService = accountService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AccountDto>>> Get()
    {
        var dto = await _accountService.GetUserAccountsAsync();

        var accounts = dto.ProjectTo<AccountDto>(Mapper.ConfigurationProvider);

        return Ok(accounts);
    }

    [HttpPost]
    public async Task<long> Post([FromBody] AccountDto dto)
    {
        var account = Mapper.Map<Account>(dto);
        await _accountService.CreateAsync(account);
        return account.Id;
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        var result = await _accountService.DeleteAsync(id);
        if (result.Success)
        {
            return NoContent();
        }

        return BadRequest(result.Errors);
    }
}