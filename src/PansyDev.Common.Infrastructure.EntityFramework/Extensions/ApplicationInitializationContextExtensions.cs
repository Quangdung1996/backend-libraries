using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace PansyDev.Common.Infrastructure.EntityFramework.Extensions
{
    public static class ApplicationInitializationContextExtensions
    {
        public static void InitializeDatabase<TContext>(this ApplicationInitializationContext context)
            where TContext : AbpDbContext<TContext>
        {
            using var scope = context.ServiceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<TContext>();
            dbContext.Database.Migrate();

            var dataSeeder = scope.ServiceProvider.GetRequiredService<IDataSeeder>();
            dataSeeder.SeedAsync().Wait();
        }
    }
}
