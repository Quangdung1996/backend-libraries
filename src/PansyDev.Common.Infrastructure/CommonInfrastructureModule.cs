using System.IO;
using PansyDev.Common.Application;
using Volo.Abp.Auditing;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Validation;

namespace PansyDev.Common.Infrastructure
{
    [DependsOn(typeof(CommonApplicationModule))]
    [DependsOn(typeof(AbpAutoMapperModule))]
    public class CommonInfrastructureModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpValidationOptions>(options => options.IgnoredTypes.Add(typeof(Stream)));
            Configure<AbpAuditingOptions>(options => options.IgnoredTypes.Add(typeof(Stream)));
        }
    }
}
