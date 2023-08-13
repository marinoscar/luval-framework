using Luval.Framework.Security.Authorization.Entities;
using Microsoft.EntityFrameworkCore;

namespace Luval.Framework.Security.Authorization
{
    public interface IAuthorizationDbContext
    {
        DbSet<Account> Accounts { get; set; }
        DbSet<SecurityClaim> SecurityClaims { get; set; }
        DbSet<SecurityRoleClaim> SecurityRoleClaims { get; set; }
        DbSet<SecurityRole> SecurityRoles { get; set; }
        DbSet<SecurityToken> SecurityTokens { get; set; }
        DbSet<UserRole> UserRoles { get; set; }
        DbSet<User> Users { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}