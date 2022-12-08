namespace MyFinance.Domain.Models.Identity;

public class AppUserRole
{
    public string AppUserId { get; set; }
    public AppUser AppUser { get; set; }

    public long AppRoleId { get; set; }
    public AppRole AppRole { get; set; }
}