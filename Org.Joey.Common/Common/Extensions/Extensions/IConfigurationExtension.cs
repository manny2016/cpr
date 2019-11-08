

namespace Org.Joey.Common
{
    using Microsoft.Extensions.Configuration;
    using System.Linq;
    public static class IConfigurationExtension
    {
        public static bool IsDevelopment(this IConfiguration configuration)
        {
            var environment = configuration.Enviroment();
            return configuration.Enviroment().Equals("Development");            
        }
        public static string Enviroment(this IConfiguration configuration)
        {
            if (configuration == null) return "Configuration is null";
            var section = configuration.GetSection("EnvironmentVariables");
            if (section == null) return "EnvironmentVariables did not configured";
            if (!string.IsNullOrEmpty(section.GetValue<string>("ASPNETCORE_ENVIRONMENT")))
                return section.GetValue<string>("ASPNETCORE_ENVIRONMENT");

            if (!string.IsNullOrEmpty(section.GetValue<string>("Environment")))
                return section.GetValue<string>("Environment");

            return "EnvironmentVariables did not configured";
        }
    }
}
