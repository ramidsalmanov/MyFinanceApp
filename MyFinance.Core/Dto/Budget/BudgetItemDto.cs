namespace MyFinance.Core.Dto.Budget;

public class BudgetItemDto : BaseDto
{
    //public CategoryDto Category { get; set; }
    public long CategoryId { get; set; }
    public decimal Plan { get; set; }
}