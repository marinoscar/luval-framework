using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luval.Framework.Services
{
    /// <summary>
    /// Provides configuration options for a service
    /// </summary>
    public class ServiceConfiguration
    {
        /// <summary>
        /// Creates a new instance
        /// </summary>
        public ServiceConfiguration()
        {
            Settings = new Dictionary<string, string>();
            NumberOfRetries = 3;
            RetryIntervalInMs = 500;
        }

        /// <summary>
        /// Gets a value indicating if the service will retry if there is a fail
        /// </summary>
        public bool RetryOnFail { get { return NumberOfRetries > 0; } }
        /// <summary>
        /// Gets or sets the number of milliseconds to wait before a retry
        /// </summary>
        public int RetryIntervalInMs { get; set; }
        /// <summary>
        /// Gets or sets the number of retries
        /// </summary>
        public int NumberOfRetries { get; set; }

        /// <summary>
        /// Gets a <see cref="Dictionary{String, String}"/> to set or get other settings for the service
        /// </summary>
        public Dictionary<string, string> Settings { get; }
    }
}
