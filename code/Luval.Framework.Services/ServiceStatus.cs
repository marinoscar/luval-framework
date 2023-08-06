using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luval.Framework.Services
{
    /// <summary>
    /// Status of the service
    /// </summary>
    public enum ServiceStatus
    {
        /// <summary>
        /// No status available
        /// </summary>
        None = 99,
        /// <summary>
        /// Failed
        /// </summary>
        Fail = 0,
        /// <summary>
        /// Completed
        /// </summary>
        Completed = 1,
        /// <summary>
        /// Service is running
        /// </summary>
        Running = 2,
        /// <summary>
        /// Service is pending
        /// </summary>
        Pending = 3
    }
}
