using FluentValidation;
using Volo.Abp.Domain;
using Volo.Abp.FluentValidation;
using Volo.Abp.Modularity;

namespace PansyDev.Common.Domain
{
    [DependsOn(typeof(AbpDddDomainModule))]
    [DependsOn(typeof(AbpFluentValidationModule))]
    public class CommonDomainModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            ValidatorOptions.Global.LanguageManager.Enabled = false;
        }
    }
}
