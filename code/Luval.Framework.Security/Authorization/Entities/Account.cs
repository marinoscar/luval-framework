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
        public string Type { get; set; }
        public DateTime? UtcExpiresOn { get; set; }
        public string? JsonData { get; set; }
    }
}
