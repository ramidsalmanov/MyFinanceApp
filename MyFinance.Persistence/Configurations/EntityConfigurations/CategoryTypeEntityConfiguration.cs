using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyFinance.Domain.Models;

namespace MyFinance.Persistence.Configurations.EntityConfigurations;

public class CategoryTypeEntityConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Path).HasColumnType("ltree");

        builder.HasData(
            new Category()
            {
                Id = 1,
                Name = "Доходы",
                Path = "Доходы",
                Type = CategoryType.Incomes
            },
            new Category()
            {
                Id = 2,
                Name = "Раcходы",
                Path = "Раcходы",
                Type = CategoryType.Expenses
            }
        );
    }
}