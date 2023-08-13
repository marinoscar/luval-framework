using Luval.Framework.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luval.Framework.Security.Authorization.Entities
{
    public class SecurityRoleClaim : IdentityEntity
    {
        [ForeignKey("SecurityRole")]
        public string SecurityRoleId { get; set; }
        public virtual SecurityRole SecurityRole { get; set; }
        [Required]
        public virtual SecurityClaim SecurityClaim { get; set; }
        [ForeignKey("SecurityClaim")]
        public string SecurityClaimId { get; set; }
    }
}
