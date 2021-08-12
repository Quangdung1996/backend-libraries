using Microsoft.Extensions.DependencyInjection;
using PansyDev.Common.Web.GraphQL.Services;
using Volo.Abp.Modularity;

namespace PansyDev.Common.Web.GraphQL
{
    // ReSharper disable once InconsistentNaming
    [DependsOn(typeof(CommonWebModule))]
    public class CommonWebGraphQLModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddErrorFilter<ErrorFilter>();
        }
    }
}
