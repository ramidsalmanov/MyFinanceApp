using MyFinance.Common.Models;
using MyFinance.Core.Dto;
using MyFinance.Domain.Models.Transactions;

namespace MyFinance.Core.Services;

public interface ITransactionService
{
    IQueryable<Transaction> GetTransactions(long accountId);
    Task<ActionResult> AddTransactionAsync(Transaction transaction);
    Task<ActionResult> AddTransactionsAsync(long accountId, IEnumerable<TransactionDto> transactions);
    Task<ActionResult> AddTransferAsync(Transfer transfer);
}