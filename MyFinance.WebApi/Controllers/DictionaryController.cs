using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyFinance.Domain.Models;
using MyFinance.Persistence;

namespace MyFinance.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DictionaryController : ControllerBase
{
    private IDbContext _context;

    public DictionaryController(IDbContext context)
    {
        _context = context;
    }

    [HttpGet("account-types")]
    [ResponseCache(Duration = 604800)]
    public async Task<IEnumerable<AccountType>> GetAccountTypes()
    {
        return await _context.Set<AccountType>().ToListAsync();
    }

    [HttpGet("currencies")]
    [ResponseCache(Duration = 604800)]
    public async Task<IEnumerable<Currency>> GetCurrencies()
    {
        return await _context.Set<Currency>().ToListAsync();
    }
}