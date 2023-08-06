using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luval.Framework.Core.Configuration
{
    /// <summary>
    /// Provides an internal memory implementation to store configuration settings
    /// </summary>
    public class MemoryConfigurationProvider : IConfigurationProvider
    {

        /// <summary>
        /// Creates a new instance
        /// </summary>
        public MemoryConfigurationProvider() : this(new Dictionary<string, string>())
        {
            
        }

        protected MemoryConfigurationProvider(Dictionary<string, string> internalData)
        {
            Internal = internalData;
        }

        protected virtual Dictionary<string, string> Internal { get; private set; }


        /// <inheritdoc/>
        public virtual string GetOrDefault(string name, string defaultValue)
        {
            if(!Internal.ContainsKey(name)) return defaultValue;
            return Internal[name];
        }

        /// <inheritdoc/>
        public string GetOrDefault(string name)
        {
            if (!Internal.ContainsKey(name)) return null;
            return Internal[name];
        }

        /// <inheritdoc/>
        public virtual void Persist()
        {
        }

        /// <inheritdoc/>
        public virtual void Set(string name, string value)
        {
            Internal[name] = value;
        }
    }
}
