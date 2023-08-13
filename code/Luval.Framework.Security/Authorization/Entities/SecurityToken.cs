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
    public class SecurityToken : IdentityEntity
    {
        [Required, MaxLength(2500)]
        public string Token { get; set; }
        public DateTime UtcExpireOn { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual User User { get; set; }
    }
}
