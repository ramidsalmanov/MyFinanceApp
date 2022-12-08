using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyFinance.Common;
using MyFinance.Common.Models;
using MyFinance.Core.Dto;
using MyFinance.Core.Services;
using MyFinance.Domain.Models;
using MyFinance.Domain.Models.Transactions;
using MyFinance.Persistence;

namespace MyFinance.WebApi.Services;

internal class TransactionService : ITransactionService
{
    private IDbContext _context;
    private IMapper _mapper;
    private IAccountService _accountService;

    private DbSet<Transaction> Transactions => _context.Set<Transaction>();

    public TransactionService(IDbContext context, IMapper mapper, IAccountService accountService)
    {
        _context = context;
        _mapper = mapper;
        _accountService = accountService;
    }

    public IQueryable<Transaction> GetTransactions(long accountId)
    {
        return Transactions.Where(x => x.AccountId == accountId);
    }

    public async Task<ActionResult> AddTransactionAsync(Transaction transaction)
    {
        await Transactions.AddAsync(transaction);
        await _context.SaveChangesAsync();
        return ActionResult.Ok(transaction);
    }

    public async Task<ActionResult> AddTransactionsAsync(long accountId, IEnumerable<TransactionDto> transactions)
    {
        Checker.IsTrue(accountId != default, "account id cannot be default");
        transactions = transactions.ToList();
        if (transactions.Count() == 0)
        {
            return ActionResult.Ok();
        }

        if (transactions.Any(x => x.AccountId == default))
        {
            ExceptionHelper.Throw<ArgumentException>("account id cannot be default");
        }

        var account = await _accountService.GetAccountAsync(accountId)!;
        Checker.NotNull(account);


        var total = transactions.Select(x =>
        {
            var sum = x.Sum;
            if (x.Category.Type == CategoryType.Expenses)
            {
                sum = -sum;
            }

            return sum;
        }).Sum();

        var entities = _mapper.Map<IEnumerable<Transaction>>(transactions).ToList();
        foreach (var transaction in entities)
        {
            account.Transactions.Add(transaction);
        }

        account.Balance = account.Balance + total;
        await _accountService.UpdateAsync(account);
        return ActionResult.Ok(entities.Select(x => x.Id));
    }

    public async Task<ActionResult> AddTransferAsync(Transfer transfer)
    {
        //await Transfers.AddAsync(transfer);
        await _context.SaveChangesAsync();
        return ActionResult.Ok(transfer);
    }
}