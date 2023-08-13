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
    public class User : StringAuditEntry
    {
        public User()
        {
            
        }

        public User(OAuthUser oAuthUser)
        {
            Name = oAuthUser.Name;
            Email = oAuthUser.Email;
            ProviderName = oAuthUser.ProviderName;
            ProviderKey = oAuthUser.ProviderKey;
            ProfilePicUrl = oAuthUser.ProfileUrl;
            Surname = oAuthUser.Surname;
        }

        [ForeignKey("Account"), Required]
        public string AccountId { get; set; }
        public virtual Account Account { get; set; }
        [Required]
        public string Name { get; set; }
        public string Surname { get; set; }

        [Index(IsUnique = true)]
        public string Email { get; set; }
        [Required]
        public string ProviderName { get; set; }
        [Index, Required]
        public string ProviderKey { get; set; }
        public string? ProfilePicUrl { get; set; }
        public string? JsonData { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
