using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using QuickbaseWebdriverIO.Attributes;

namespace QuickbaseWebdriverIO.Services
{
    public class ConfigurationService
    {
        static ConfigurationService()
        {
            Root = InitializeConfiguration();
        }

        public static IConfigurationRoot Root { get; set; }

        /// <summary>
        /// You need to provide a section names for collections in the settings.
        /// </summary>
        /// <typeparam name="TSection">Settings section.</typeparam>
        /// <param name="sectionName">Settings collection name.</param>
        /// <returns>Object or collection of objects from the settings.</returns>
        public static TSection GetSection<TSection>(string sectionName = null)
          where TSection : class, new()
        {
            if (string.IsNullOrEmpty(sectionName))
            {
                sectionName = MakeFirstLetterToLower(typeof(TSection).Name);
            }

            return Root.GetSection(sectionName).Get<TSection>();
        }

        public static string GetSettingsByProperty(PropertyInfo property)
        {
            string headerName = string.Empty;
            var headerNameAttribute = property.GetCustomAttributes(typeof(SettingsAttribute)).FirstOrDefault();
            if (headerNameAttribute != null)
            {
                headerName = ((SettingsAttribute)headerNameAttribute).SettingName;
                return headerName;
            }

            return headerName;
        }

        public static string FindSettingsFileName()
        {
            var executionDir = new DirectoryInfo(Directory.GetCurrentDirectory());
            var filesInExecutionDir = Directory.GetFiles(executionDir.FullName);

            return Environment.GetEnvironmentVariable("ExecutionEnvironment") ??
                   filesInExecutionDir.FirstOrDefault(x => x.Contains($"testSettings.{executionDir.Parent.Name}.json"));
        }

        private static string MakeFirstLetterToLower(string text)
        {
            return char.ToLower(text[0]) + text[1..];
        }

        private static IConfigurationRoot InitializeConfiguration()
        {
            var builder = new ConfigurationBuilder();
            var settingsName = FindSettingsFileName();

            if (!string.IsNullOrEmpty(settingsName))
            {
                builder.AddJsonFile(settingsName, optional: true, reloadOnChange: true);
            }
            else
            {
                throw new Exception("Settings files are not copied into the bin folder. You need to set \"Copy to Output Directory\"");
            }

            builder.AddEnvironmentVariables();

            return builder.Build();
        }
    }
}
