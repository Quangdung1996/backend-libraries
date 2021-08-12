using PansyDev.Common.Domain;
using Volo.Abp.Application;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace PansyDev.Common.Application
{
    [DependsOn(typeof(CommonDomainModule))]
    [DependsOn(typeof(AbpDddApplicationModule))]
    [DependsOn(typeof(AbpAutofacModule))]
    public class CommonApplicationModule : AbpModule { }
}
