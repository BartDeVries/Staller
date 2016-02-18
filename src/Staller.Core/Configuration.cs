using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Staller.Core
{
    public static class Configuration
    {
        private static IConfigurationRoot configuration;

        static Configuration()
        {
            var builder = new ConfigurationBuilder()
            .AddJsonFile("config.json")
            .AddJsonFile("config.local.json", optional: true)
            .AddEnvironmentVariables();
            configuration = builder.Build();

            ConnectionStrings = new ConnectionStringConfiguration(configuration);
            Label = new LabelConfiguration(configuration);
        }

        public static ConnectionStringConfiguration ConnectionStrings { get; set; }

        public static LabelConfiguration Label { get; set; }

        public class ConnectionStringConfiguration
        {
            private IConfigurationRoot configuration;

            public ConnectionStringConfiguration(IConfigurationRoot configuration)
            {
                this.configuration = configuration;
            }


            public string AzureStorage { get { return configuration.GetSection("ConnectionStrings:StorageConnectionString").Value; } }
        }


        public class LabelConfiguration
        {
            private IConfigurationRoot configuration;

            public LabelConfiguration(IConfigurationRoot configuration)
            {
                this.configuration = configuration;
            }

            public string Id { get { return configuration.GetSection("Label:Id").Value; } }

            public string Name { get { return configuration.GetSection("Label:Name").Value; } }
        }
    }
}
