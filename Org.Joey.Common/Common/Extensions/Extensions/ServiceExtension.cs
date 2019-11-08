

namespace Org.Joey.Common
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using System;
    public static partial class ServiceExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="configure">return configuration file path</param>
        /// <returns></returns>
        public static IServiceCollection AddJsonConfiguration(this IServiceCollection collection, Func<string> configure = null)
        {
            var configurationFile = configure?.Invoke();
            if (string.IsNullOrEmpty(configurationFile))
            {
#if DEBUG
                configurationFile = "appsettings.Development.json";
#else
               
 configurationFile = "appsettings.json";
#endif
            }

            var configuration = new ConfigurationBuilder()
                 .AddJsonFile(configurationFile, true, true)
                 .Build();
            collection.AddSingleton(typeof(IConfiguration), configuration);
            return collection;
        }

    }
}
