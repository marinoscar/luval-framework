using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Luval.Framework.Core.Configuration
{
    public static class JsonFileConfigurationExtensions
    {
        /// <summary>
        /// Loads the configuration or creates a new one
        /// </summary>
        /// <param name="p">Instance</param>
        /// <param name="fileName">The name of the configuration file, not the full path, just the name</param>
        /// <param name="envName">The environment name to use i.e. dev, test. prod</param>
        /// <param name="isSecret">Indicates if the config file contains secrets and needs to be a separate file to be excluded from version control, if true, the file name will end on *-secrets.json</param>
        /// <returns>A new instance of <see cref="JsonConfigurationProvider"/> with a json file</returns>
        public static JsonFileConfigurationProvider LoadOrCreate(this JsonFileConfigurationProvider p, string fileName, string? envName, bool isSecret = false)
        {
            if(string.IsNullOrWhiteSpace(fileName)) throw new ArgumentNullException(nameof(fileName));

            var env = envName?.ToLowerInvariant();
            var name = fileName.ToLowerInvariant();
            var sec = isSecret ? "secrets" : string.Empty;
            var parts = (new[] { name, env, sec }).Where(i => string.IsNullOrWhiteSpace(i));
            var fileInfo = new FileInfo(Path.Combine(Environment.CurrentDirectory, $"{string.Join('-', parts)}.json"));
            if (!fileInfo.Exists) File.WriteAllText(fileInfo.FullName, "{ \"key\" : \"value\" }");
            return new JsonFileConfigurationProvider(fileInfo);
        }

        /// <summary>
        /// Loads the configuration or creates a new one
        /// </summary>
        /// <param name="p">Instance</param>
        /// <param name="envName">The environment name to use i.e. dev, test. prod</param>
        /// <param name="isSecret">Indicates if the config file contains secrets and needs to be a separate file to be excluded from version control, if true, the file name will end on *-secrets.json</param>
        /// <returns>A new instance of <see cref="JsonConfigurationProvider"/> with a json file</returns>
        public static JsonFileConfigurationProvider LoadOrCreate(this JsonFileConfigurationProvider p, string? envName, bool isSecret = false)
        {
            return LoadOrCreate(p, fileName: Assembly.GetExecutingAssembly().GetName().Name?.ToLowerInvariant(), envName, isSecret);
        }

        /// <summary>
        /// Loads the configuration or creates a new one for the development environment
        /// </summary>
        /// <param name="p">Instance</param>
        /// <param name="isSecret">Indicates if the config file contains secrets and needs to be a separate file to be excluded from version control, if true, the file name will end on *-dev-secrets.json</param>
        /// <returns>A new instance of <see cref="JsonConfigurationProvider"/> with a json file</returns>
        public static JsonFileConfigurationProvider LoadOrCreateDev(this JsonFileConfigurationProvider p, bool isSecret = false)
        {
            return LoadOrCreate(p, "dev", isSecret);
        }

        /// <summary>
        /// Loads the configuration or creates a new one for the testing environment
        /// </summary>
        /// <param name="p">Instance</param>
        /// <param name="isSecret">Indicates if the config file contains secrets and needs to be a separate file to be excluded from version control, if true, the file name will end on *-test-secrets.json</param>
        /// <returns>A new instance of <see cref="JsonConfigurationProvider"/> with a json file</returns>
        public static JsonFileConfigurationProvider LoadOrCreateTest(this JsonFileConfigurationProvider p, bool isSecret = false)
        {
            return LoadOrCreate(p, "test", isSecret);
        }

        /// <summary>
        /// Loads the configuration or creates a new one for the testing environment
        /// </summary>
        /// <param name="p">Instance</param>
        /// <param name="isSecret">Indicates if the config file contains secrets and needs to be a separate file to be excluded from version control, if true, the file name will end on *-prod-secrets.json</param>
        /// <returns>A new instance of <see cref="JsonConfigurationProvider"/> with a json file</returns>
        public static JsonFileConfigurationProvider LoadOrCreateProd(this JsonFileConfigurationProvider p, bool isSecret = false)
        {
            return LoadOrCreate(p, "prod", isSecret);
        }
    }
}
