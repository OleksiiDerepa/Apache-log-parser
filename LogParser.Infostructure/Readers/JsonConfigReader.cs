using System.IO;
using LogParser.Infrastructure.Validation;
using Microsoft.Extensions.Configuration;

namespace LogParser.Infrastructure.Readers
{
    public class JsonConfigReader
    {
        public const string ConfigFileName = "appsettings.json";

        private readonly IConfigurationRoot _configuration;

        public JsonConfigReader(string  filePath)
        {
            Require.NotEmpty(filePath,()=>$"{filePath} should be specified");
            Require.IsTrue(File.Exists(filePath),$"Specified file for configure this app: {filePath} does not exist");

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(filePath, optional: true, reloadOnChange: true);

            _configuration = builder.Build();
        }

        public T GetValue<T>(string name)
        {
            return _configuration.GetValue<T>(name);
        } 

        public string GetConnectionString(string name)
        {
            return _configuration.GetConnectionString(name);
        } 

        public T GetTypedSection<T>(string name) where T : new()
        {
            var section = new T();
            _configuration.GetSection(name).Bind(section);

            return section;
        }
    }
}