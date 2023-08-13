using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luval.Framework.Data.Entities
{
    public class IdentityEntity : AuditEntity<ulong>
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override ulong Id { get => base.Id; set => base.Id = value; }
    }
}
