using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace Luval.Framework.Data.Entities
{
    public class AuditEntity<TKeyType>
    {
        public AuditEntity()
        {
            UtcCreatedOn = DateTime.UtcNow;
            UtcUpdatedOn = UtcCreatedOn;
            Version = 1;
        }

        [Key]
        public virtual TKeyType Id { get; set; }
        public DateTime? UtcCreatedOn { get; set; }

        public string? CreatedBy { get; set; }
        public DateTime? UtcUpdatedOn { get; set; }
        public string? UpdatedBy { get; set; }
        public int? Version { get; set; }


        public void MarkForUpdate()
        {
            MarkForUpdate(null);
        }
        public void MarkForUpdate(string updatedBy)
        {
            UtcUpdatedOn = DateTime.UtcNow;
            Version = Version + 1;
            if(!string.IsNullOrWhiteSpace(updatedBy))
                UpdatedBy = updatedBy;
        }
    }
}
