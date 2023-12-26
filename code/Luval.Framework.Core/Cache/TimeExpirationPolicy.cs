using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luval.Framework.Core.Cache
{
    public class TimeExpirationPolicy : IExpirationPolicy
    {

        public TimeExpirationPolicy() : this(TimeSpan.Zero, DateTime.MinValue)
        {
        }

        public TimeExpirationPolicy(TimeSpan duration, DateTime utcCreatedOn)
        {
            this.Duration = duration;
            this.UtcCreatedOn = utcCreatedOn;
        }

        public TimeSpan Duration { get; private set; }
        public DateTime UtcCreatedOn { get; private set; }

        public IExpirationPolicy DeSerialize(string content)
        {
            return JsonConvert.DeserializeObject<TimeExpirationPolicy>(content);
        }

        public bool HasExpired()
        {
            if (Duration == TimeSpan.Zero || Duration == TimeSpan.MaxValue || Duration == TimeSpan.MinValue) return false;
            return DateTime.UtcNow.Subtract(UtcCreatedOn) > Duration;
        }

        public string Serialize()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
