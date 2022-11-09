using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Currency = MyFinance.Domain.Models.Currency;

namespace MyFinance.Persistence.Configurations.EntityConfigurations;

public class CurrencyTypeEntityConfiguration: IEntityTypeConfiguration<Currency>
{
    public void Configure(EntityTypeBuilder<Currency> builder)
    {
        builder.HasData(new[]
        {
            new Currency()
            {
                Id = 1,
                Code = 643,
                CodeString = "RUB",
                Name = "Руб"
            },
            new Currency()
            {
                Id = 2,
                Code = 840,
                CodeString = "RUB",
                Name = "Доллар"
            },
            new Currency()
            {
                Id = 3,
                Code = 978,
                CodeString = "RUB",
                Name = "Евро"
            },
        });
    }
}