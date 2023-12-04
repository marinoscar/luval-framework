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


        public ChronTimeHostedService(ILogger logger, string chronExpression, string timeZoneId, TimeSpan dueTime, TimeSpan period) :
            this(logger, chronExpression, TimeZoneInfo.FindSystemTimeZoneById(timeZoneId), dueTime, period)
        {

        }
        public ChronTimeHostedService(ILogger logger, string chronExpression, TimeZoneInfo timeZoneInfo, TimeSpan dueTime, TimeSpan period) : 
            base(logger, dueTime, period)
        {
            ChronExpression = CronExpression.Parse(chronExpression);
            TimeZone = timeZoneInfo;
        }

        protected override void OnTimerTick(object? state)
        {
            var now = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZone).TrimSec();
            var d = ChronExpression.GetNextOccurrence(DateTime.UtcNow)?.TrimSec();
            if (d == null)
            {
                Logger.LogError($"Invalid Chron Expression on {GetType().Name}");
                return;
            }
            d = TimeZoneInfo.ConvertTimeFromUtc(d.Value, TimeZone);

            if (NextChronOcurrence == null) NextChronOcurrence = d;

            Logger.LogDebug($"Actual: {now} => Class: { NextChronOcurrence } => Chron: { d }");

            if (now == NextChronOcurrence)
            {
                Logger.LogDebug("Running Task");
                //Starts an async process
                Task.Run(() => DoWork());
            }
            
            //Update the occurrence
            if (d != NextChronOcurrence) NextChronOcurrence = d;

        }
    }
}
