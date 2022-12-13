using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyFinance.Core.Dto;
using MyFinance.Core.Dto.Account;
using MyFinance.Core.Services;
using MyFinance.Domain.Models.Transactions;

namespace MyFinance.WebApi.Controllers;

[Route("api/{controller}")]
public class TransactionsController : BaseController            
{
    private ITransactionService    _transactionService;
    private IMapper _mapper;

    public TransactionsController(IMapper mapper, ITransactionService transactionService)
    {
        _mapper = mapper;
        _transactionService = transactionService;
    }

    [HttpGet("{id}")]
    public async Task<IEnumerable<TransactionDto>> Get(long id)
    {
        var transactions = await _transactionService.GetTransactions(id)
            .ProjectTo<TransactionDto>(_mapper.ConfigurationProvider).ToListAsync();

        return transactions;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] AccountDto accountDto)
    {
        var result = await _transactionService.AddTransactionsAsync(accountDto.Id, accountDto.Transactions);

        if (result.Success)
        {
            return Ok(result.GetData<IEnumerable<long>>());
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError("", error);
        }

        return Problem();
    }

    [HttpPost]
    [Route("transfer")]
    public async Task<ActionResult<Transfer>> Post(TransferDto dto)
    {
        var transfer = _mapper.Map<Transfer>(dto);
        var result = await _transactionService.AddTransferAsync(transfer);
        if (result.Failure)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }

            return Problem();
        }

        return Ok(result.GetData<Transfer>());
    }
}