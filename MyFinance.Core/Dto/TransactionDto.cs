namespace MyFinance.Core.Dto;

public class TransactionDto : BaseDto
{
    public DateTime CreateDate { get; set; }
    public decimal Sum { get; set; }
    public string Comment { get; set; }
    public long AccountId { get; set; }
    public CategoryDto Category { get; set; }
}