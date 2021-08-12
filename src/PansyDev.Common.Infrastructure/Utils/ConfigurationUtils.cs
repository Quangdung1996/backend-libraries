using Microsoft.Extensions.Configuration;

namespace PansyDev.Common.Infrastructure.Utils
{
    public static class ConfigurationUtils
    {
        public static IConfiguration GetUserSecrets<T>() where T : class
        {
            var builder = new ConfigurationBuilder();
            builder.AddUserSecrets<T>();

            return builder.Build();
        }
    }
}
