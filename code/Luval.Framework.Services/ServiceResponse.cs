using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luval.Framework.Services
{
    /// <summary>
    /// Represents the response of a service call
    /// </summary>
    public class ServiceResponse<T>
    {
        /// <summary>
        /// Gets or sets the <see cref="ServiceStatus"/>
        /// </summary>
        public ServiceStatus Status { get; set; }
        /// <summary>
        /// Gets or sets the message for the service response
        /// </summary>
        public string? Message { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Exception"/> for a service response
        /// </summary>
        public Exception? Exception { get; set; }

        /// <summary>
        /// Gets or sets the result object for the service response
        /// </summary>
        public T? Result { get; set; }

    }
}
