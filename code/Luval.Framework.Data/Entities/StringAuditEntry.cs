using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luval.Framework.Data.Entities
{
    public class StringAuditEntry : AuditEntity<string>
    {
        public StringAuditEntry() : base()
        {
            Id = Guid.NewGuid().ToString().Replace("-", "").ToLowerInvariant();
        }
    }
}
