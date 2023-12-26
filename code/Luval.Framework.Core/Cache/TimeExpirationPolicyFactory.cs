using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luval.Framework.Core.Cache
{
    public class TimeExpirationPolicyFactory : IExpirationPolicyFactory
    {
        public TimeSpan Duration { get; set; }

        public TimeExpirationPolicyFactory(TimeSpan duration)
        {
            this.Duration = duration;
        }

        public IExpirationPolicy Create()
        {
            return new TimeExpirationPolicy(Duration, DateTime.UtcNow);
        }
    }
}
