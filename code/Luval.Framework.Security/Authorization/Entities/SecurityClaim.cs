using Luval.Framework.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luval.Framework.Security.Authorization.Entities
{
    public class SecurityClaim : StringAuditEntry
    {
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }

        internal static List<SecurityClaim> GetInitialValues()
        {
            return new List<SecurityClaim>() {
                new SecurityClaim() { Name = "Access" },
                new SecurityClaim() { Name = "Navigate" },
                new SecurityClaim() { Name = "Read" },
                new SecurityClaim() { Name = "Edit" },
                new SecurityClaim() { Name = "Delete" },
                new SecurityClaim() { Name = "Configure" },
                new SecurityClaim() { Name = "Admin" }
            };
        }
    }
}
