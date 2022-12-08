using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyFinance.Domain.Models.Budget;

namespace MyFinance.Persistence.Configurations.EntityConfigurations;

public class BudgetItemEntityConfiguration : IEntityTypeConfiguration<BudgetItem>
{
    public void Configure(EntityTypeBuilder<BudgetItem> builder)
    {
        builder.HasIndex(x => new { x.BudgetId, x.CategoryId }).IsUnique();
    }
}