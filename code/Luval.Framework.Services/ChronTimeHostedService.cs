using Cronos;
using Luval.Framework.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luval.Framework.Services
{
    public abstract class ChronTimeHostedService : TimedHostedService
    {
        protected virtual DateTime? NextChronOcurrence { get; private set; }
        protected virtual CronExpression ChronExpression { get; private set; }
        public TimeZoneInfo TimeZone { get; set; }


        public ChronTimeHostedService(ILogger logger, string chronExpression, string timeZoneId, TimeSpan period) :
            this(logger, chronExpression, TimeZoneInfo.FindSystemTimeZoneById(timeZoneId), period)
        {

        }
        public ChronTimeHostedService(ILogger logger, string chronExpression, TimeZoneInfo timeZoneInfo, TimeSpan period) : 
            base(logger, DateTime.UtcNow.AddMinutes(1).TrimSec().Subtract(DateTime.UtcNow), period)
        {
            ChronExpression = CronExpression.Parse(chronExpression);
            TimeZone = timeZoneInfo;
        }

        protected override void OnTimerTick(object? state)
        {
            var now = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZone).TrimMs();
            var d = ChronExpression.GetNextOccurrence(DateTime.UtcNow)?.TrimMs();
            if (d == null)
            {
                Logger.LogError($"Invalid Chron Expression on {GetType().Name}");
                return;
            }
            d = TimeZoneInfo.ConvertTimeFromUtc(d.Value, TimeZone);

            if (NextChronOcurrence == null) NextChronOcurrence = d;
            
            if (now == NextChronOcurrence)
            {
                //Starts an async process
                Task.Run(() => DoWork());
            }
            
            //Update the occurrence
            if (d != NextChronOcurrence) NextChronOcurrence = d;

        }
    }
}
