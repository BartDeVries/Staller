using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Staller.Core
{
    /// <summary>
    /// Static instance of the Configuration.
    /// </summary>
    public class Configuration
    {
        private static volatile Configuration instance;
        private static object syncRoot = new Object();

        
        public static Configuration Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new Configuration();
                    }
                }

                return instance;
            }
        }


        private static IConfigurationRoot configuration;

        /// <summary>
        /// Instantiate right away.
        /// </summary>
        private Configuration()
        {
            var builder = new ConfigurationBuilder()
            .AddJsonFile("config.json")
            .AddJsonFile("config.local.json", optional: true)
            .AddEnvironmentVariables();
            configuration = builder.Build();

            ConnectionStrings = new ConnectionStringConfiguration(configuration.GetSection("ConnectionStrings"));
            Label = new LabelConfiguration(configuration.GetSection("Label"));
            Authentication = new AuthenticationConfiguration(configuration.GetSection("Authentication"));
        }

        /// <summary>
        /// Connections
        /// </summary>
        public ConnectionStringConfiguration ConnectionStrings { get; set; }

        /// <summary>
        /// Label configuration
        /// </summary>
        public LabelConfiguration Label { get; set; }

        /// <summary>
        /// Authentication
        /// </summary>
        public AuthenticationConfiguration Authentication { get; set; }







        /// <summary>
        /// Configuration for storing ConnectionStrings. i.e. to Azure Tables.
        /// </summary>
        public class ConnectionStringConfiguration : ConfigurationSectionBase
        {
            public ConnectionStringConfiguration(IConfigurationSection configuration) : base(configuration)
            {
                AzureStorage = configuration.GetSection("StorageConnectionString").Value;
            }

            /// <summary>
            /// Connectionstring to an AzureStorage account
            /// </summary>
            public readonly string AzureStorage;
        }

        /// <summary>
        /// Configuration for storing Label information. For multi-/whitelabel purposes.
        /// </summary>
        public class LabelConfiguration : ConfigurationSectionBase
        {
            public LabelConfiguration(IConfigurationSection configuration) : base(configuration)
            {
                Id = configuration.GetSection("Id").Value;
                Name = configuration.GetSection("Name").Value;
            }

            /// <summary>
            /// Id of the label. For referencing; should not change.
            /// </summary>
            public readonly string Id;

            /// <summary>
            /// Name of the label for presentation.
            /// </summary>
            public readonly string Name;
        }

        /// <summary>
        /// Configuration for storing Authentication related keys
        /// </summary>
        public class AuthenticationConfiguration : ConfigurationSectionBase
        {
            public AuthenticationConfiguration(IConfigurationSection configuration) : base(configuration)
            {
                Google = new GoogleAuthenticationConfiguration(configuration.GetSection("Google"));
            }

            /// <summary>
            /// Google authentication
            /// </summary>
            public GoogleAuthenticationConfiguration Google { get; set; }


            /// <summary>
            /// Configuration for Google authentication related keys.
            /// </summary>
            public class GoogleAuthenticationConfiguration : ConfigurationSectionBase
            {
                public GoogleAuthenticationConfiguration(IConfigurationSection configuration) : base(configuration)
                {
                    ClientId = configuration.GetSection("ClientId").Value;
                    ClientSecret = configuration.GetSection("ClientSecret").Value;
                }

                /// <summary>
                /// Client application id
                /// </summary>
                public readonly string ClientId;

                /// <summary>
                /// Client secret
                /// </summary>
                public readonly string ClientSecret;
            }
        }
    }

    /// <summary>
    /// Abstract class for all configuration sections.
    /// </summary>
    public abstract class ConfigurationSectionBase
    {
        private IConfigurationSection configuration;

        /// <summary>
        /// Instantiate supplying the section it is about.
        /// </summary>
        /// <param name="configuration"></param>
        public ConfigurationSectionBase(IConfigurationSection configuration)
        {
            this.configuration = configuration;
        }
    }
}
