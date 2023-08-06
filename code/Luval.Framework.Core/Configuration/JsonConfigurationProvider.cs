using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luval.Framework.Core.Configuration
{
    public class JsonConfigurationProvider : MemoryConfigurationProvider
    {
        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="jsonContent">The json content to parse and create the object</param>
        public JsonConfigurationProvider(string jsonContent) : base(Create(jsonContent)) { 
        }

        private static Dictionary<string, string> Create(string jsonContent)
        {
            if(string.IsNullOrWhiteSpace(jsonContent)) throw new ArgumentNullException(nameof(jsonContent));
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonContent);
        }
    }
}
