using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.PostgreSql;
using Volo.Abp.Modularity;

namespace PansyDev.Common.Infrastructure.EntityFramework
{
    [DependsOn(typeof(CommonInfrastructureModule))]
    [DependsOn(typeof(AbpEntityFrameworkCoreModule))]
    [DependsOn(typeof(AbpEntityFrameworkCorePostgreSqlModule))]
    public class CommonInfrastructureEntityFrameworkModule : AbpModule { }
}
