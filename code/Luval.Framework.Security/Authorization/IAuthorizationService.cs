using Luval.Framework.Security.Authorization.Entities;

namespace Luval.Framework.Security.Authorization
{
    public interface IAuthorizationService
    {
        IAuthorizationDbContext Context { get; }

        Task<User> GetApplicationUserAync(string email, CancellationToken cancellationToken);
        Task<User> RegisterUserAsync(OAuthUser user, CancellationToken cancellationToken);
    }
}