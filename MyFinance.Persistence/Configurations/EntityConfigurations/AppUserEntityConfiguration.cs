using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyFinance.Domain.Models.Identity;

namespace MyFinance.Persistence.Configurations.EntityConfigurations;

public class AppUserEntityConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.ToTable("AppUsers");
        builder
            .HasMany(x => x.Roles)
            .WithMany(x => x.Users)
            .UsingEntity<AppUserRole>(
                u => u.HasOne(x => x.AppRole).WithMany(),
                r => r.HasOne(x => x.AppUser).WithMany());
        var user = new AppUser()
        {
            Id = APP_USER_ID,
            UserName = "admin",
            NormalizedUserName = "ADMIN",
            PasswordHash = "AQAAAAEAACcQAAAAEFKiPKdTwYTgnb3tUwlk3GKf6NSwfc1kY3JQSFx8O/mWJaWupuHyiwICxVGqR1jkCQ==",
            SecurityStamp = string.Empty,
            ConcurrencyStamp = string.Empty
        };
        builder.HasData(user);
    }

    public const string APP_USER_ID = "4A065F13-D011-437E-B22A-8B4666C5E5CE";
}