using Luval.Framework.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luval.Framework.Security.Authorization.Entities
{
    public class Account : StringAuditEntry
    {

        [Index]
        public string Name { get; set; }
        public AccountType Type { get; set; }
        public DateTime? UtcExpiresOn { get; set; }
        public string? JsonData { get; set; }
    }

    public enum AccountType { Free, Tier1, Tier2, Tier3, Tier4, Tier5, Tier6, Tier7, Tier8, Tier9 }
}
