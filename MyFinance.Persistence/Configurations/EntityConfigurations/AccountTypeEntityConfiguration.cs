using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AccountType = MyFinance.Domain.Models.AccountType;

namespace MyFinance.Persistence.Configurations.EntityConfigurations;

public class AccountTypeEntityConfiguration: IEntityTypeConfiguration<AccountType>
{
    public void Configure(EntityTypeBuilder<AccountType> builder)
    {
        builder.HasData(
            new List<AccountType>()
            {
                new()
                {
                    Id = 1,
                    Name = "Кошелёк"
                },
                new()
                {
                    Id = 2,
                    Name = "Дебетовая карта"
                },
                new()
                {
                    Id = 3,
                    Name = "Кредит"
                },
                new()
                {
                    Id = 4,
                    Name = "Кредитная карта"
                },
                new()
                {
                    Id = 5,
                    Name = "Долг"
                },
            });
    }
}