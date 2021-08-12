using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace PansyDev.Common.Infrastructure.EntityFramework.Extensions
{
    public static class ServiceConfigurationContextExtensions
    {
        public static void ConfigureDatabase<TContext>(this ServiceConfigurationContext context)
            where TContext : AbpDbContext<TContext>
        {
            var configuration = context.Services.GetConfiguration();
            var connectionString = configuration.GetConnectionString("Default");

            context.Services.AddPooledDbContextFactory<TContext>(
                builder => builder.UseNpgsql(connectionString));

            context.Services.AddAbpDbContext<TContext>(builder => builder.AddDefaultRepositories(true));

            context.Services.Configure<AbpDbContextOptions>(options => options.UseNpgsql());
        }
    }
}
