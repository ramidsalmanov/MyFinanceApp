using MyFinance.Domain.Models.Identity;
using MyFinance.Domain.Models.Transactions;

namespace MyFinance.Domain.Models;

public class Account : EntityBase
{
    public string Name { get; set; }
    public decimal Balance { get; set; }
    public string Comment { get; set; }

    public long TypeId { get; set; }
    public AccountType Type { get; set; }

    public long CurrencyId { get; set; }
    public Currency Currency { get; set; }

    public string AppUserId { get; set; }
    public AppUser AppUser { get; set; }

    public ICollection<Transaction> Transactions { get; set; }
    public ICollection<Transfer> Transfers { get; set; }

    public Account()
    {
        Transactions = new HashSet<Transaction>();
        Transfers = new HashSet<Transfer>();
    }
}