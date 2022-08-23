using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace WebFileSystem.Presentation.Configurations
{
    public static class AppsettingsProvider
    {
        public static IConfigurationRoot GetJsonAppsettingsFile() => new ConfigurationBuilder()
           .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
           .AddJsonFile("appsettings.json", false)
           .Build();
    }
}
