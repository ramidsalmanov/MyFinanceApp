using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyFinance.Domain.Models.Identity;

namespace MyFinance.Persistence.Configurations.EntityConfigurations;

public class AppRoleTypeEntityConfiguration : IEntityTypeConfiguration<AppRole>
{
    public void Configure(EntityTypeBuilder<AppRole> builder)
    {
        builder.HasData(new List<AppRole>()
        {
            new()
            {
                Id = 1,
                Name = "Admin"
            }
        });
    }
}