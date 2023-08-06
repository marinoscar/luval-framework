using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luval.Framework.Services
{
    public class ServiceStatusChangeEventArgs : EventArgs
    {
        /// <summary>
        /// Creates a new instance
        /// </summary>
        public ServiceStatusChangeEventArgs(ServiceStatus status)
        {
            Status = status;
        }

        /// <summary>
        /// Gets the status
        /// </summary>
        public ServiceStatus Status { get; private set; }
    }
}
