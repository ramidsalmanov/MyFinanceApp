namespace MyFinance.Domain.Models.Identity;

public class AppRole : EntityBase
{
    public string Name { get; set; }
    public string Description { get; set; }

    public ICollection<AppUser> Users { get; set; }
}