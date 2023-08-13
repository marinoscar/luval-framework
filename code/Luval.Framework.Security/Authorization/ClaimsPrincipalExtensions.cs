using Luval.Framework.Security.Authorization.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Luval.Framework.Security.Authorization
{
    public static class ClaimsPrincipalExtensions
    {
        public static OAuthUser ToOAuthUser(this ClaimsPrincipal p, string providerName = "Google")
        {
            return new OAuthUser(p, providerName);
        }
    }
}
