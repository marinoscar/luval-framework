using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luval.Framework.Core.Configuration
{
    /// <summary>
    /// Provides an implementation for a json file configuration file
    /// </summary>
    public class JsonFileConfigurationProvider : JsonConfigurationProvider
    {

        private FileInfo _file;

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="file">The file object to extract the data from</param>
        public JsonFileConfigurationProvider(FileInfo file) : base(GetContent(file))
        {
            _file = file;
        }

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="jsonFileName">The full path to the file to get the data from</param>
        public JsonFileConfigurationProvider(string jsonFileName) : this(new FileInfo(jsonFileName))
        {
            
        }

        /// <summary>
        /// Persists the values into the json file
        /// </summary>
        public override void Persist()
        {
            var json = JsonConvert.SerializeObject(Internal);
            File.WriteAllText(json, _file.FullName);
        }

        private static string GetContent(FileInfo file)
        {
            if (file == null) throw new ArgumentNullException(nameof(file));
            if (!file.Exists) throw new FileNotFoundException($"File {file.FullName} doesn't exists");
            return File.ReadAllText(file.FullName);
        }
    }
}
