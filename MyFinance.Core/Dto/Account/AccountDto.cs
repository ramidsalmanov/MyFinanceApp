using MyFinance.Core.Dto.Common;

namespace MyFinance.Core.Dto.Account;

public class AccountDto : BaseDto
{
    public string Name { get; set; }
    public EntityWithNameDto Type { get; set; }
    public CurrencyDto Currency { get; set; }
    public decimal Balance { get; set; }
    public string Comment { get; set; }
    public string UserId { get; set; }

    public List<TransactionDto> Transactions { get; set; }
}