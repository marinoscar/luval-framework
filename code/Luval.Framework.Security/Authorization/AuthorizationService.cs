using Luval.Framework.Security.Authorization.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Luval.Framework.Security.Authorization
{
    public class AuthorizationService : IAuthorizationService
    {

        public AuthorizationService(IAuthorizationDbContext context)
        {
            Context = context;
        }

        public IAuthorizationDbContext Context { get; private set; }

        /// <summary>
        /// This method will make sure the <see cref="ClaimsPrincipal"/> user is registered in the application
        /// </summary>
        /// <param name="user">The <see cref="Entities.IAuthorizationDbContext"/> user to register</param>
        /// <returns></returns>
        public Task<User> RegisterUserAsync(OAuthUser user, CancellationToken cancellationToken)
        {
            return GetOrCreateUserAsync(user, cancellationToken);
        }


        /// <summary>
        /// This method will retrieve and instance of the <see cref="User"/> 
        /// </summary>
        /// <param name="email">The user email to use to search the data repository</param>
        /// <returns>An instance of the <see cref="User"/></returns>
        public Task<User> GetApplicationUserAync(string email, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                return Context.Users.FirstOrDefault(i => i.Email == email);
            }, cancellationToken);
        }

        protected virtual async Task<User> GetOrCreateUserAsync(OAuthUser oAuthUser, CancellationToken cancellationToken)
        {
            bool hasChanges = false;
            var user = await GetApplicationUserAync(oAuthUser.Email, cancellationToken);
            if (user == null)
            {
                var account = await GetDefaultAccount(oAuthUser.Email, cancellationToken);
                user = Context.Users.Add(new User(oAuthUser) { AccountId = account.Id, Account = account }).Entity;
                await Context.SaveChangesAsync(cancellationToken);
                await GetUserRoles(user, cancellationToken);
            }
            if (user.ProfilePicUrl != oAuthUser.ProfileUrl)
            {
                user.ProfilePicUrl = oAuthUser.ProfileUrl;
                hasChanges = true;
            }
            if (user.Name != oAuthUser.Name)
            {
                user.Name = oAuthUser.Name;
                hasChanges = true;
            }
            if (user.Surname != oAuthUser.Surname)
            {
                user.Surname = oAuthUser.Surname;
                hasChanges = true;
            }
            if (hasChanges)
            {
                user.MarkForUpdate();
                await Context.SaveChangesAsync(cancellationToken);
            }
            return user;
        }

        protected virtual async Task<Account> GetDefaultAccount(string email, CancellationToken cancellationToken)
        {
            var account = Context.Accounts.FirstOrDefault(i => i.Name == email);
            if (account == null)
            {
                account = Context.Accounts.Add(new Account() { Name = email, Type = "Free", CreatedBy = "System", UpdatedBy = "System" }).Entity;
                await Context.SaveChangesAsync(cancellationToken);
            }
            return account;
        }

        protected virtual async Task<IEnumerable<UserRole>> GetUserRoles(User user, CancellationToken cancellationToken)
        {
            var userRoles = Context.UserRoles.Where(i => i.UserId == user.Id);
            if (userRoles == null || !userRoles.Any())
            {
                Context.UserRoles.Add(new UserRole()
                {
                    Role = await GetDefaultRole(cancellationToken),
                    User = user,
                    CreatedBy = "System",
                    UpdatedBy = "System"
                });
                await Context.SaveChangesAsync(cancellationToken);
                return Context.UserRoles.Where(i => i.UserId == user.Id);
            }
            return userRoles;
        }

        protected virtual async Task<SecurityRole> GetDefaultRole(CancellationToken cancellationToken)
        {
            var role = Context.SecurityRoles.FirstOrDefault(i => i.Name == "User");
            if (role == null)
            {
                role = Context.SecurityRoles.Add(new SecurityRole() { Name = "User" }).Entity;
                await Context.SaveChangesAsync(cancellationToken);
            }
            if (role.Claims == null || !role.Claims.Any())
            {
                var claim = await GetDefaultClaim(cancellationToken);
                Context.SecurityRoleClaims.Add(new SecurityRoleClaim() { SecurityClaim = claim, SecurityRole = role, CreatedBy = "System", UpdatedBy = "System" });
                await Context.SaveChangesAsync(cancellationToken);
            }
            return role;
        }

        protected virtual async Task<SecurityClaim> GetDefaultClaim(CancellationToken cancellationToken)
        {
            if (!Context.SecurityClaims.Any())
            {
                Context.SecurityClaims.Add(new SecurityClaim() { Name = "navigation", CreatedBy = "System", UpdatedBy = "System" });
                Context.SecurityClaims.Add(new SecurityClaim() { Name = "read", CreatedBy = "System", UpdatedBy = "System" });
                Context.SecurityClaims.Add(new SecurityClaim() { Name = "write", CreatedBy = "System", UpdatedBy = "System" });
                Context.SecurityClaims.Add(new SecurityClaim() { Name = "delete", CreatedBy = "System", UpdatedBy = "System" });
                await Context.SaveChangesAsync(cancellationToken);
            }
            var claim = Context.SecurityClaims.FirstOrDefault(i => i.Name == "navigation");
            if (claim == null)
            {
                claim = Context.SecurityClaims.Add(new SecurityClaim() { Name = "navigation", CreatedBy = "System", UpdatedBy = "System" }).Entity;
                await Context.SaveChangesAsync(cancellationToken);
            }
            return claim;
        }
    }
}
