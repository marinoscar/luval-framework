using Microsoft.AspNetCore.Authentication;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Luval.Framework.Security.Authorization
{
    public class ClaimsTransformation : IClaimsTransformation
    {
        private readonly IAuthorizationService _service;

        public ClaimsTransformation(IAuthorizationService service)
        {
            _service = service;
        }

        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            // Clone current identity
            var clone = principal.Clone();
            var newIdentity = (ClaimsIdentity)clone.Identity;
            var email = clone.FindFirst(ClaimTypes.Email)?.Value;
            if (string.IsNullOrWhiteSpace(email)) return principal;

            var user = await _service.GetApplicationUserAync(email, CancellationToken.None);
            if (user == null) return principal;

            var items = new Dictionary<string, string>();
            if (!string.IsNullOrWhiteSpace(user.JsonData))
                items = JsonConvert.DeserializeObject<Dictionary<string, string>>(user.JsonData);

            if (string.IsNullOrWhiteSpace(user.TimeZoneName) && newIdentity.HasClaim(LuvalClaimTypes.TimeZone, user.TimeZoneName))
            {
                newIdentity.AddClaim(new Claim(LuvalClaimTypes.TimeZone, user.TimeZoneName));
            }

            if (user.TimeZoneOffset != null && newIdentity.HasClaim(LuvalClaimTypes.TimeZoneOffset, user.TimeZoneOffset?.ToString()))
            {
                newIdentity.AddClaim(new Claim(LuvalClaimTypes.TimeZoneOffset, user.TimeZoneOffset.ToString()));
            }

            if (string.IsNullOrWhiteSpace(user.JsonData) && newIdentity.HasClaim(LuvalClaimTypes.UserCustomSettings, user.JsonData))
            {
                newIdentity.AddClaim(new Claim(LuvalClaimTypes.UserCustomSettings, user.JsonData));
            }


            foreach (var c in items)
            {
                if (newIdentity.HasClaim(c.Key, c.Value)) continue;
                newIdentity.AddClaim(new Claim(c.Key, c.Value));
            }

            foreach (var c in user.UserRoles)
            {
                if (c.Role == null) continue;
                if (newIdentity.HasClaim(ClaimTypes.Role, c.Role?.Name)) continue;
                newIdentity.AddClaim(new Claim(ClaimTypes.Role, c.Role?.Name));
            }

            return clone;
        }
    }
}
