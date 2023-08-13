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
    public class UserDevice : IdentityEntity
    {

        [Required, ForeignKey("User")]
        public string UserId { get; set; }

        public virtual User User { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        [Required, MaxLength(200)]
        public string PushEndpoint { get; set; }
        [Required, MaxLength(200)]
        public string PushP256DH { get; set; }

        [Required, MaxLength(200)]
        public string PushAuth { get; set; }
    }
}
