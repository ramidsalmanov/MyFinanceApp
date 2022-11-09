using System.ComponentModel.DataAnnotations.Schema;
using MyFinance.Domain.Models.Identity;

namespace MyFinance.Domain.Models.Budget;

[Table("Budgets")]
public class Budget : EntityBase
{
    public string Name { get; set; }
    public string AppUserId { get; set; }
    public AppUser AppUser { get; set; }

    public ICollection<BudgetItem> BudgetItems { get; set; }

    public Budget()
    {
        BudgetItems = new HashSet<BudgetItem>();
    }
}