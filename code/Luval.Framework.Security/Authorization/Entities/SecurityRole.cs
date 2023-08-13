using Luval.Framework.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luval.Framework.Security.Authorization.Entities
{
    public class SecurityRole : StringAuditEntry
    {
        [Index]
        public string Name { get; set; }
        public virtual ICollection<SecurityClaim> Claims { get; set; }

        internal static List<SecurityRole> GetInitialValues()
        {
            return new List<SecurityRole>() {
                new SecurityRole() { Name = "User"  }
            };
        }


    }
}
