namespace MyFinance.Domain.Models.Transactions;

public class Transaction : EntityBase
{
    public DateTime CreateDate { get; set; }
    public string Comment { get; set; }

    public virtual long AccountId { get; set; }
    public virtual Account Account { get; set; }
    public decimal Sum { get; set; }
    public long CategoryId { get; set; }
    public Category Category { get; set; }
}