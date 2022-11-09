namespace MyFinance.Domain.Models.Transactions;

public class Transfer : EntityBase
{
    public DateTime CreateDate { get; set; }
    public string Comment { get; set; }

    public virtual long AccountId { get; set; }
    public virtual Account Account { get; set; }
    
    public decimal Sum { get; set; }

    public long SourceAccountId { get; set; }
    public long DestinationAccountId { get; set; }
}