using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace PansyDev.Common.Domain
{
    [DependsOn(typeof(AbpDddDomainModule))]
    public class CommonDomainModule : AbpModule { }
}
