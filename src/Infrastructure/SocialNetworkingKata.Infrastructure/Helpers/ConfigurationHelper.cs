using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialNetworkingKata.Infrastructure.Helpers
{
    public static class ConfigurationHelper
    {
        public static IConfigurationRoot BuildConfiguration(string baseDirectory)
        {
            return new ConfigurationBuilder()
                .SetBasePath(baseDirectory)
                .AddJsonFile("appsettings.json", optional: true)
                .Build();
        }
    }
}
