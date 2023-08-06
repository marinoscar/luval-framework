using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luval.Framework.Core.Configuration
{
    /// <summary>
    /// Provides an implementation of environment values
    /// </summary>
    public class EnvVariableConfigurationProvider : MemoryConfigurationProvider
    {
        /// <inheritdoc/>
        public override string GetOrDefault(string name, string defaultValue)
        {
            var val = Environment.GetEnvironmentVariable(name);
            if (!string.IsNullOrEmpty(val)) Internal[name] = val;
            return base.GetOrDefault(name, defaultValue);
        }


        /// <summary>
        /// Persists the environment values
        /// </summary>
        public override void Persist()
        {
            foreach (var kv in Internal)
            {
                Environment.SetEnvironmentVariable(kv.Key, kv.Value);
            }
        }
    }
}
