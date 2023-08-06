using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luval.Framework.Services
{
    public class ServiceFailEventArgs : EventArgs
    {
        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="exception">The <see cref="Exception"/></param>
        public ServiceFailEventArgs(Exception exception)
        {
            Exception = exception;
        }

        /// <summary>
        /// Gets the <see cref="Exception"/> for the service
        /// </summary>
        public Exception Exception { get; private set; }
    }
}
