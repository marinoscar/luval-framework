using Cronos;
using Luval.Framework.Core;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luval.Framework.Services.Utilities
{
    public class ChronEvaluator
    {
        public ChronEvaluator(string chronExpression, string timeZone) : this(chronExpression, TimeZoneInfo.FindSystemTimeZoneById(timeZone))
        {

        }

        public ChronEvaluator(string chronExpression, TimeZoneInfo timeZone) : this(chronExpression, timeZone, new EmptyLogger())
        {

        }

        public ChronEvaluator(string chronExpression, string timeZone, ILogger logger) : this(chronExpression, TimeZoneInfo.FindSystemTimeZoneById(timeZone), logger)
        {

        }

        public ChronEvaluator(string chronExpression, TimeZoneInfo timeZone, ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            ChronExpression = CronExpression.Parse(chronExpression ?? throw new ArgumentNullException(nameof(chronExpression)));
            TimeZone = timeZone ?? throw new ArgumentNullException(nameof(timeZone));
        }

        private readonly ILogger _logger;

        protected virtual DateTime? NextChronOcurrence { get; private set; }
        public virtual TimeZoneInfo TimeZone { get; private set; }
        public virtual CronExpression ChronExpression { get; private set; }

        public bool Evaluate(DateTime utcDateTime, bool includeMs, bool includeSeconds)
        {
            var chron = ChronExpression.GetNextOccurrence(DateTime.UtcNow);
            if (chron == null) throw new ArgumentNullException("Invalid chron expression");

            if (!includeMs)
            {
                utcDateTime = utcDateTime.TrimMs();
                chron = chron.Value.TrimMs();
            }
            if (!includeSeconds)
            {
                utcDateTime = utcDateTime.TrimSec();
                chron = chron.Value.TrimSec();
            }

            if (NextChronOcurrence == null) 
                NextChronOcurrence = chron;

            var evalTime = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, TimeZone);

            var result = evalTime == NextChronOcurrence;

            _logger.LogDebug($"Utc: {utcDateTime} Local: {evalTime} => Ocurrence: {NextChronOcurrence} => Chron: {chron} Result: {result}");

            if (chron != NextChronOcurrence)
                NextChronOcurrence = chron;

            return result;
        }

        public bool EvaluateNow(bool includeMs, bool includeSeconds)
        {
            return Evaluate(DateTime.UtcNow, includeMs, includeSeconds);
        }

    }
}
