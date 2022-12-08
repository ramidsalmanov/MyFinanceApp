using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyFinance.Domain.Models.Identity;

namespace MyFinance.Persistence.Configurations.EntityConfigurations;

public class AppUserRoleTypeEntityConfiguration : IEntityTypeConfiguration<AppUserRole>
{
    public void Configure(EntityTypeBuilder<AppUserRole> builder)
    {
        builder.HasData(new AppUserRole[]
        {
            new()
            {
                AppRoleId = 1,
                AppUserId = AppUserEntityConfiguration.APP_USER_ID
            }
        });
    }
}