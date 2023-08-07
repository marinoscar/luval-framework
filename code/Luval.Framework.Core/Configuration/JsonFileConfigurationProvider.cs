using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

        /// <summary>
        /// Loads the configuration or creates a new one
        /// </summary>
        /// <param name="fileName">The name of the configuration file, not the full path, just the name</param>
        /// <param name="envName">The environment name to use i.e. dev, test. prod</param>
        /// <param name="isSecret">Indicates if the config file contains secrets and needs to be a separate file to be excluded from version control, if true, the file name will end on *-secrets.json</param>
        /// <returns>A new instance of <see cref="JsonConfigurationProvider"/> with a json file</returns>
        public static JsonFileConfigurationProvider LoadOrCreate(string fileName, string? envName, bool isSecret = false)
        {
            if (string.IsNullOrWhiteSpace(fileName)) throw new ArgumentNullException(nameof(fileName));

            var exeFileName = new FileInfo(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            var env = envName?.ToLowerInvariant();
            var name = fileName.ToLowerInvariant();
            var sec = isSecret ? "secrets" : string.Empty;
            var parts = (new[] { name, env, sec }).Where(i => !string.IsNullOrWhiteSpace(i)).ToList();
            var fileInfo = new FileInfo(Path.Combine(exeFileName.DirectoryName, $"{string.Join('-', parts)}.json"));
            if (!fileInfo.Exists) File.WriteAllText(fileInfo.FullName, "{ \"key\" : \"value\" }");
            return new JsonFileConfigurationProvider(fileInfo);
        }

        /// <summary>
        /// Loads the configuration or creates a new one
        /// </summary>
        /// <param name="envName">The environment name to use i.e. dev, test. prod</param>
        /// <param name="isSecret">Indicates if the config file contains secrets and needs to be a separate file to be excluded from version control, if true, the file name will end on *-secrets.json</param>
        /// <returns>A new instance of <see cref="JsonConfigurationProvider"/> with a json file</returns>
        public static JsonFileConfigurationProvider LoadOrCreate(string? envName, bool isSecret = false)
        {
            return LoadOrCreate(fileName: System.Diagnostics.Process.GetCurrentProcess().MainModule.ModuleName?.ToLowerInvariant(), envName, isSecret);
        }

        /// <summary>
        /// Loads the configuration or creates a new one for the development environment
        /// </summary>
        /// <param name="isSecret">Indicates if the config file contains secrets and needs to be a separate file to be excluded from version control, if true, the file name will end on *-dev-secrets.json</param>
        /// <returns>A new instance of <see cref="JsonConfigurationProvider"/> with a json file</returns>
        public static JsonFileConfigurationProvider LoadOrCreateDev(bool isSecret = false)
        {
            return LoadOrCreate("dev", isSecret);
        }

        /// <summary>
        /// Loads the configuration or creates a new one for the testing environment
        /// </summary>
        /// <param name="isSecret">Indicates if the config file contains secrets and needs to be a separate file to be excluded from version control, if true, the file name will end on *-test-secrets.json</param>
        /// <returns>A new instance of <see cref="JsonConfigurationProvider"/> with a json file</returns>
        public static JsonFileConfigurationProvider LoadOrCreateTest(bool isSecret = false)
        {
            return LoadOrCreate("test", isSecret);
        }

        /// <summary>
        /// Loads the configuration or creates a new one for the testing environment
        /// </summary>
        /// <param name="isSecret">Indicates if the config file contains secrets and needs to be a separate file to be excluded from version control, if true, the file name will end on *-prod-secrets.json</param>
        /// <returns>A new instance of <see cref="JsonConfigurationProvider"/> with a json file</returns>
        public static JsonFileConfigurationProvider LoadOrCreateProd(bool isSecret = false)
        {
            return LoadOrCreate("prod", isSecret);
        }
    }
}
