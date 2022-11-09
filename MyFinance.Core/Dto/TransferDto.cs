namespace MyFinance.Core.Dto;

public class TransferDto : BaseDto
{
    public DateTime CreateDate { get; set; }
    public decimal Sum { get; set; }
    public string Comment { get; set; }

    public long SourceAccountId { get; set; }
    public long DestinationAccountId { get; set; }
    public long AccountId { get; set; }
}