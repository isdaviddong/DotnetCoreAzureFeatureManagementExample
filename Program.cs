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
                            var connection = "Endpoint=https://testappconfig20201025.azconfig.io;Id=AULg-lb-s0:dc80GXPyMTCbgi+WF9Fl;Secret=OYPevymfwZJayZ5HOg0b9OuQBSBbC8dFjjFUh2MpxQ0=";
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
