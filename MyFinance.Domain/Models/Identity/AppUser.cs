﻿namespace MyFinance.Domain.Models.Identity;

public class AppUser
{
    public string Id { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
    public string UserName { get; set; }
    public string NormalizedUserName { get; set; }
    public string Email { get; set; }
    public string NormalizedEmail { get; set; }
    public bool EmailConfirmed { get; set; }
    public string PasswordHash { get; set; }
    public string SecurityStamp { get; set; }
    public string ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();
    public string PhoneNumber { get; set; }
    public bool PhoneNumberConfirmed { get; set; }
    public bool TwoFactorEnabled { get; set; }
    public DateTimeOffset? LockoutEnd { get; set; }
    public bool LockoutEnabled { get; set; }
    public int AccessFailedCount { get; set; }
    public override string ToString() => UserName;

    public ICollection<AppRole> Roles { get; set; }
    public ICollection<Category> Categories { get; set; }
    public ICollection<Account> Accounts { get; set; }

    public AppUser()
    {
        Id = Guid.NewGuid().ToString();
        Accounts = new HashSet<Account>();
        Categories = new HashSet<Category>();
        Roles = new HashSet<AppRole>();
    }
}