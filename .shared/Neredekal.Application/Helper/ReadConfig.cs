using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace Neredekal.Application.Helper
{
    public static class ReadConfig
    {
        public static T Get<T>(string section, string fileName = "appsettings.json")
        {
            var builder = CreateConfigurationBuilder(fileName);
            var configuration = builder.Build();

            return configuration.GetSection(section).Get<T>();
        }

        public static T Get<T>(List<string> sections, string fileName = "appsettings.json")
        {
            var builder = CreateConfigurationBuilder(fileName);
            var configuration = builder.Build();

            var configurationSection = configuration.GetSection(sections[0]);

            if (sections.Count > 1)
                sections.ForEach(q => configurationSection = configurationSection.GetSection(q));

            return configurationSection.Get<T>();
        }

        public static string GetConnectionString(string key) => CreateConfigurationBuilder().Build().GetSection("ConnectionStrings").GetSection(key).Value;

        public static string GetConnectionString(string key, string fileName) => CreateConfigurationBuilder(fileName).Build().GetSection("ConnectionStrings")[key];

        public static string CustomSectionValue(string section, string key) => CreateConfigurationBuilder().Build().GetSection(section)[key];

        private static ConfigurationBuilder CreateConfigurationBuilder(string fileName = "appsettings.json")
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile(fileName, optional: false, reloadOnChange: true);

            return builder;
        }
    }
}