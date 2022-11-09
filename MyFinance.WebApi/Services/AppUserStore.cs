using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyFinance.Domain.Models.Identity;
using MyFinance.Persistence;

namespace MyFinance.WebApi.Services;

public class AppUserStore : IUserPasswordStore<AppUser>, IUserRoleStore<AppUser>
{
    private IDbContext _context;
    private IRoleStore<AppRole> _userRoles;
    private DbSet<AppUser> Users => _context.Set<AppUser>();

    public AppUserStore(IDbContext context, IRoleStore<AppRole> userRoles)
    {
        _context = context;
        _userRoles = userRoles;
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    public Task<string> GetUserIdAsync(AppUser user, CancellationToken cancellationToken)
    {
        return Task.FromResult(user.Id);
    }

    public Task<string> GetUserNameAsync(AppUser user, CancellationToken cancellationToken)
    {
        return Task.FromResult(user.UserName);
    }

    public Task SetUserNameAsync(AppUser user, string userName, CancellationToken cancellationToken)
    {
        user.UserName = userName;
        Users.Update(user);
        return _context.SaveChangesAsync(cancellationToken);
    }

    public Task<string> GetNormalizedUserNameAsync(AppUser user, CancellationToken cancellationToken)
    {
        return Task.FromResult(user.NormalizedUserName);
    }

    public Task SetNormalizedUserNameAsync(AppUser user, string normalizedName, CancellationToken cancellationToken)
    {
        user.NormalizedUserName = normalizedName;
        return Task.CompletedTask;
    }

    public async Task<IdentityResult> CreateAsync(AppUser user, CancellationToken cancellationToken)
    {
        Users.Add(user);
        try
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception e)
        {
            return IdentityResult.Failed(new[]
            {
                new IdentityError()
                {
                    Description = e.Message
                }
            });
        }

        return IdentityResult.Success;
    }

    public async Task<IdentityResult> UpdateAsync(AppUser user, CancellationToken cancellationToken)
    {
        Validate(user);
        user.ConcurrencyStamp = new Guid().ToString();
        Users.Update(user);
        try
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception e)
        {
            return IdentityResult.Failed(new IdentityError()
            {
                Description = e.Message
            });
        }

        return IdentityResult.Success;
    }

    public async Task<IdentityResult> DeleteAsync(AppUser user, CancellationToken cancellationToken)
    {
        Validate(user);
        Users.Remove(user);
        try
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception e)
        {
            return IdentityResult.Failed(new[]
            {
                new IdentityError()
                {
                    Description = e.Message
                }
            });
        }

        return IdentityResult.Success;
    }

    public async Task<AppUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
    {
        return (await Users.FindAsync(userId))!;
    }

    public Task<AppUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
    {
        return Users.FirstOrDefaultAsync(x => x.NormalizedUserName == normalizedUserName)!;
    }

    public Task SetPasswordHashAsync(AppUser user, string passwordHash, CancellationToken cancellationToken)
    {
        user.PasswordHash = passwordHash;
        return Task.CompletedTask;
    }

    public Task<string> GetPasswordHashAsync(AppUser user, CancellationToken cancellationToken)
    {
        return Task.FromResult(user.PasswordHash);
    }

    public Task<bool> HasPasswordAsync(AppUser user, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task AddToRoleAsync(AppUser user, string roleName, CancellationToken cancellationToken)
    {
        // cancellationToken.ThrowIfCancellationRequested();
        // Validate(user);
        // if (string.IsNullOrWhiteSpace(roleName))
        // {
        //     throw new ArgumentException("roleName cannot be null or empty", nameof(roleName));
        // }
        //
        // var role = await _context.Set<AppRole>().FirstOrDefaultAsync(x => x.Name.Equals(roleName));
        // if (role == null)
        // {
        //     throw new ArgumentException($"role with {roleName} not found");
        // }
        //
        //
        //

        throw new NotImplementedException();
    }

    public Task RemoveFromRoleAsync(AppUser user, string roleName, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<IList<string>> GetRolesAsync(AppUser user, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        var userId = user.Id;
        var query = from userRole in _context.Set<AppUserRole>()
            join role in _context.Set<AppRole>() on userRole.AppRoleId equals role.Id
            where userRole.AppUserId.Equals(userId)
            select role.Name;
        return await query.ToListAsync(cancellationToken);
    }

    public Task<bool> IsInRoleAsync(AppUser user, string roleName, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IList<AppUser>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public void Validate(AppUser? user)
    {
        if (user == null || string.IsNullOrEmpty(user.UserName))
        {
            throw new ArgumentException("wrong user params");
        }
    }
}