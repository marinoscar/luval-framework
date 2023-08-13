using Luval.Framework.Security.Authorization.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luval.Framework.Security.Authorization
{
    public class AuthorizationDbContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<SecurityClaim> SecurityClaims { get; set; }
        public DbSet<SecurityRoleClaim> SecurityRoleClaims { get; set; }
        public DbSet<SecurityRole> SecurityRoles { get; set; }
        public DbSet<SecurityToken> SecurityTokens { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<User> Users { get; set; }

        public virtual async Task<int> SeedDataAsync(CancellationToken cancellationToken = default)
        {
            if (!SecurityRoles.Any())
                await SecurityRoles.AddRangeAsync(SecurityRole.GetInitialValues(), cancellationToken);
            if(!SecurityClaims.Any())
                await SecurityClaims.AddRangeAsync(SecurityClaim.GetInitialValues(), cancellationToken);
            return await SaveChangesAsync(cancellationToken);
        }
    }
}
