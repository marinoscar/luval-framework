using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luval.Framework.Core.Configuration
{
    public static class ConfigManager
    {
        private static IConfigurationProvider? _config;

        public static void Init(IConfigurationProvider configurationProvider)
        {

            _config = configurationProvider ?? throw new ArgumentNullException(nameof(configurationProvider));
        }

        /// <summary>
        /// Gets the setting
        /// </summary>
        /// <param name="name">The name for the setting to retrieve</param>
        /// <returns>The value for the setting</returns>
        public static string Get(string name)
        {
            return GetOrDefault(name, null);
        }


        /// <summary>
        /// Gets the setting, if not found returns a default value
        /// </summary>
        /// <param name="name">The name for the setting to retrieve</param>
        /// <param name="defaultValue">A default value in case the setting is not available</param>
        /// <returns>The value for the setting</returns>
        public static string GetOrDefault(string name, string defaultValue)
        {
            if (_config == null) throw new Exception("Instance not initialized, invoke the Init method");
            return _config.GetOrDefault(name, defaultValue);
        }


    }
}
