using System.Security.Claims;

namespace Luval.Framework.Security.Authorization.Entities
{
    public class OAuthUser
    {
        public OAuthUser()
        {
        }

        public OAuthUser(ClaimsPrincipal principal, string providerName)
        {
            ProviderName = providerName;
            Name = principal?.FindFirst(ClaimTypes.GivenName)?.Value;
            Surname = principal?.FindFirst(ClaimTypes.Surname)?.Value;
            Email = principal?.FindFirst(ClaimTypes.Email)?.Value;
            ProviderKey = principal?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrWhiteSpace(Name))
                Name = principal?.FindFirst(ClaimTypes.GivenName)?.Value;
            if (providerName == "Google")
                ProfileUrl = principal?.FindFirst("urn:google:image")?.Value;
        }

        public string ProviderKey { get; set; }
        public string ProviderName { get; set; }
        public string Email { get; set; }
        public string ProfileUrl { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public Dictionary<string, string> Claims { get; set; } = new Dictionary<string, string>();

        public static OAuthUser Create(ClaimsPrincipal principal, string providerName)
        {
            return new OAuthUser(principal, providerName);
        }

        public static OAuthUser CreateGoogleUser(ClaimsPrincipal principal)
        {
            return new OAuthUser(principal, "Google");
        }
    }
}