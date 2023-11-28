using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luval.Framework.Services
{
    public abstract class LuvalServiceBase<TIn, TOut> : ILuvalService<TIn, TOut>
    {

        public LuvalServiceBase(ILogger logger, string name, ServiceConfiguration serviceConfiguration)
        {
            Logger = logger;
            Name = name;
            ServiceConfiguration = serviceConfiguration;
        }

        /// <summary>
        /// Gets the service configuration
        /// </summary>
        protected virtual ServiceConfiguration ServiceConfiguration { get; private set; }

        /// <inheritdoc/>
        public string Name { get; private set; }

        /// <inheritdoc/>
        public ILogger Logger { get; private set; }

        #region Events

        /// <inheritdoc/>
        public event EventHandler<EventArgs> Started;

        /// <inheritdoc/>
        public event EventHandler<EventArgs> Completed;

        /// <inheritdoc/>
        public event EventHandler<ServiceFailEventArgs> Fail;

        /// <inheritdoc/>
        public event EventHandler<ServiceStatusChangeEventArgs> StausChange;

        protected virtual void OnStarted()
        {
            Started?.Invoke(this, new EventArgs());
            OnStatusChange(ServiceStatus.Running);
        }

        protected virtual void OnCompleted()
        {
            Completed?.Invoke(this, new EventArgs());
            OnStatusChange(ServiceStatus.Completed);
        }

        protected virtual void OnFail(Exception exception)
        {
            Fail?.Invoke(this, new ServiceFailEventArgs(exception));
            OnStatusChange(ServiceStatus.Fail);
        }

        protected virtual void OnStatusChange(ServiceStatus status)
        {
            StausChange?.Invoke(this, new ServiceStatusChangeEventArgs(status));
        }


        #endregion

        /// <inheritdoc/>
        public async Task<ServiceResponse<TOut>> ExecuteAsync(TIn input, CancellationToken cancellationToken)
        {
            var result = new ServiceResponse<TOut>();
            OnStarted();
            Logger?.LogInformation($"Starting service {Name}");
            var retryCount = 0;
            var success = true;

            while (true)
            {
                try
                {
                    result = await DoExecuteAsync(input, cancellationToken);
                    success = true;
                }
                catch (Exception ex)
                {
                    Logger?.LogError(ex, $"Exception running service {Name}");
                    result.Exception = ex;
                    result.Message = ex.Message;
                    success = false;

                    if (ServiceConfiguration.RetryOnFail)
                        Logger?.LogWarning($"Service {Name} is retrying. Attempt Number: {retryCount + 1} of {ServiceConfiguration.NumberOfRetries}");
                }

                retryCount++;
                if (success || (retryCount > ServiceConfiguration.NumberOfRetries)) break;

                await Task.Delay(ServiceConfiguration.RetryIntervalInMs);
            }

            Logger?.LogInformation($"Completed service {Name}");
            if (success)
            {
                result.Exception = null;
                result.Message = null;
                OnCompleted();
            }
            else
            {
                if (result.Exception != null)
                    OnFail(result.Exception);
            }
            return result;
        }

        protected abstract Task<ServiceResponse<TOut>> DoExecuteAsync(TIn input, CancellationToken cancellationToken);
    }
}
