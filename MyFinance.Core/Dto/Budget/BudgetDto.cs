namespace MyFinance.Core.Dto.Budget;

public class BudgetDto : BaseDto
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string Name { get; set; }
    public List<BudgetItemDto> BudgetItems { get; set; }
}