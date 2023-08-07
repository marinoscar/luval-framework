using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luval.Framework.Core.Configuration
{
    public class ConfigurationProvider : IConfigurationProvider
    {

        /// <summary>
        /// Gets the list of <see cref="IConfigurationProvider"/> in the class"
        /// </summary>
        public List<IConfigurationProvider> Providers { get; private set; }



        public ConfigurationProvider(params IConfigurationProvider[] providers)
        {
            Providers = new List<IConfigurationProvider>(providers);
        }


        /// <inheritdoc/>
        public string Get(string name)
        {
            return GetOrDefault(name, null);
        }


        /// <inheritdoc/>
        public string GetOrDefault(string name, string defaultValue)
        {
            foreach (var provider in Providers) { 
                var val = provider.Get(name);
                if(val != null) return val;
            }
            return defaultValue;
        }

        /// <summary>
        /// Method not implemented
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void Persist()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Method not implemented
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void Set(string name, string value)
        {
            throw new NotImplementedException();
        }
    }
}
