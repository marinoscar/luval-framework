using Luval.Framework.Core;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luval.Framework.Services
{
    public abstract class TimedHostedService : BackgroundService
    {
        private ulong _executionCount = 0;
        
        private Timer? _timer = null;
        private TimeSpan? _dueTime;
        private TimeSpan? _period;

        protected ILogger Logger { get; private set; }
        protected ulong ExecutionCount { get { return _executionCount; } }
    
        protected Timer? Timer { get { return _timer; } }

        public TimedHostedService(ILogger logger) : this(logger, TimeSpan.Zero, TimeSpan.FromSeconds(60))
        {
                
        }

        public TimedHostedService(ILogger logger, TimeSpan period) : this(logger, TimeSpan.Zero, period)
        {
            
        }

        public TimedHostedService(ILogger logger, TimeSpan dueTime, TimeSpan period)
        {
            Logger = logger;
            _dueTime = dueTime;
            _period = period;
        }

        protected abstract void DoWork();

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _timer = new Timer(TimerTick, null, _dueTime.Value,
                _period.Value);
            return Task.CompletedTask;
        }

        public  override Task StartAsync(CancellationToken stoppingToken)
        {
            Logger.LogInformation("Timed Hosted Service running.");

            return base.StartAsync(stoppingToken);
        }

        private void TimerTick(object? state)
        {
            var count = Interlocked.Increment(ref _executionCount);
            Logger.LogDebug("Timed Hosted Service is working. Count: {Count}", count);

            OnTimerTick(state);
        }

        protected virtual void OnTimerTick(object? state)
        {
            DoWork();
        }

        public override Task StopAsync(CancellationToken stoppingToken)
        {
            Logger.LogInformation("Timed Hosted Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return base.StopAsync(stoppingToken);
        }

        public override void Dispose()
        {
            _timer?.Dispose();
            base.Dispose();
        }
    }
}
