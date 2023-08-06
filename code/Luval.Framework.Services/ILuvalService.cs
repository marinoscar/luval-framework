using Microsoft.Extensions.Logging;

namespace Luval.Framework.Services
{
    public interface ILuvalService<T>
    {

        /// <summary>
        /// Gets the service name
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the <see cref="ILogger"/> instance
        /// </summary>
        ILogger Logger { get; }

        /// <summary>
        /// Executes the service
        /// </summary>
        Task<ServiceResponse<T>> ExecuteAsync();

        /// <summary>
        /// Indicates that the service has started
        /// </summary>
        event EventHandler<EventArgs> Started;

        /// <summary>
        /// Indicates that the service was completed
        /// </summary>
        event EventHandler<EventArgs> Completed;

        /// <summary>
        /// Indicates that the service has failed
        /// </summary>
        event EventHandler<ServiceFailEventArgs> Fail;

        /// <summary>
        /// Indicates that has been a change in status
        /// </summary>
        event EventHandler<ServiceStatusChangeEventArgs> StausChange;
    }
}