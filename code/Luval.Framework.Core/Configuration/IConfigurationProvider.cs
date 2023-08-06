using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luval.Framework.Core.Configuration
{
    /// <summary>
    /// Abtraction to get and set configuration settings
    /// </summary>
    public interface IConfigurationProvider
    {


        /// <summary>
        /// Sets a value for a setting
        /// </summary>
        /// <param name="name">The name to use to store the setting</param>
        /// <param name="value">The value to store</param>
        void Set(string name, string value);

        /// <summary>
        /// Gets the setting
        /// </summary>
        /// <param name="name">The name for the setting to retrieve</param>
        /// <returns>The value for the setting</returns>
        string GetOrDefault(string name);


        /// <summary>
        /// Gets the setting, if not found returns a default value
        /// </summary>
        /// <param name="name">The name for the setting to retrieve</param>
        /// <param name="defaultValue">A default value in case the setting is not available</param>
        /// <returns>The value for the setting</returns>
        string GetOrDefault(string name, string defaultValue);


        /// <summary>
        /// Persists the configuration into the target store
        /// </summary>
        void Persist();

    }
}
