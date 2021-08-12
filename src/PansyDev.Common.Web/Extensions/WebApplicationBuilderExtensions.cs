using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PansyDev.Common.Infrastructure;
using Volo.Abp.Modularity;

namespace PansyDev.Common.Web.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        public static void ConfigureCommonServices<TWebModule, TInfraModule>(this WebApplicationBuilder builder)
            where TWebModule : class, IAbpModule
            where TInfraModule : class, IAbpModule
        {
            builder.Host.ConfigureAppConfiguration((context, config) =>
            {
                var env = context.HostingEnvironment;
                var folder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Configuration");

                config.AddJsonFile(Path.Combine(folder, "appsettings.json"), true);
                config.AddJsonFile(Path.Combine(folder, "appsettings.core.json"));

                if (env.IsDevelopment())
                {
                    config.AddJsonFile(Path.Combine(folder, "appsettings.dev.json"), true);
                    config.AddJsonFile(Path.Combine(folder, "appsettings.core.dev.json"));
                }

                config.AddUserSecrets<CommonInfrastructureModule>(true);
                config.AddUserSecrets<TInfraModule>(true);
            });

            builder.Host.UseAutofac();

            builder.Services.AddApplication<TWebModule>();
        }
    }
}
