using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;

namespace webapp
{
    public class Program
    {
        public static IConfigurationRefresher ConfigurationRefresher { set; get; }

        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    //使用 AddAzureAppConfiguration
                    webBuilder.ConfigureAppConfiguration(
                      config =>
                        {
                            var settings = config.Build();
                            //replace your connection
                            var connection = "Endpoint=....";
                            config.AddAzureAppConfiguration(options =>
                                {
                                    options.Connect(connection).UseFeatureFlags(
                                        FeatureFlagconfig =>
                                        {
                                            FeatureFlagconfig.CacheExpirationInterval = TimeSpan.FromSeconds(5);
                                        }
                                    );
                                    //GetRefresher
                                    ConfigurationRefresher = options.GetRefresher();
                                }
                            );
                        }
                    );
                });
    }
}
