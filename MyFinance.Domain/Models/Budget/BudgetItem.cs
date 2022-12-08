namespace MyFinance.Domain.Models.Budget;

public class BudgetItem : EntityBase
{
    public long CategoryId { get; set; }
    public Category Category { get; set; }

    public long BudgetId { get; set; }
    public Budget Budget { get; set; }

    public decimal Plan { get; set; }
}