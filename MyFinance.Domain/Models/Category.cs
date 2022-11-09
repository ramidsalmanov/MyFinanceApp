using MyFinance.Domain.Models.Identity;

namespace MyFinance.Domain.Models;

public class Category : EntityBase
{
    public string Name { get; set; }
    public string Path { get; set; }
    public CategoryType Type { get; set; }

    public ICollection<AppUser> Users { get; set; }
}