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
    private ITransactionService _transactionService;

    public TransactionsController(IMapper mapper, ITransactionService transactionService) : base(mapper)
    {
        _transactionService = transactionService;
    }

    [HttpGet("{id}")]
    public async Task<IEnumerable<TransactionDto>> Get(long id)
    {
        var transactions = await _transactionService.GetTransactions(id)
            .ProjectTo<TransactionDto>(Mapper.ConfigurationProvider).ToListAsync();

        // var transfers = _transactionService.GetTransfers(id)
        //     .ProjectTo<TransactionDto>(Mapper.ConfigurationProvider);

        return transactions;
    }
    //
    // protected override async Task OnPostAsync(Transaction entity)
    // {
    //     await _transactionService.CreateAsync(entity);
    // }

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
        var transfer = Mapper.Map<Transfer>(dto);
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