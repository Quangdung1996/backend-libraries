using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace PansyDev.Common.Infrastructure.Extensions
{
    public static class ServiceConfigurationContextExtensions
    {
        public static void Configure<TOptions>(this ServiceConfigurationContext context, string sectionKey)
            where TOptions : class
        {
            var configuration = context.Services.GetConfiguration();

            var section = configuration.GetSection(sectionKey);

            context.Services.PreConfigure<TOptions>(options => section.Bind(options));
            context.Services.Configure<TOptions>(section);
        }

        public static void ConfigureMappingProfile<T>(this ServiceConfigurationContext context)
        {
            context.Services.Configure<AbpAutoMapperOptions>(options =>
            {
                options.Configurators.Add(configurationContext =>
                {
                    var profile = ActivatorUtilities.CreateInstance(configurationContext.ServiceProvider, typeof(T));
                    configurationContext.MapperConfiguration.AddProfile((Profile)profile);
                });
            });
        }
    }
}
