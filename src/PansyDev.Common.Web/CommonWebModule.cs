using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using PansyDev.Common.Infrastructure;
using Volo.Abp;
using Volo.Abp.AspNetCore;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace PansyDev.Common.Web
{
    [DependsOn(typeof(CommonInfrastructureModule))]
    [DependsOn(typeof(AbpAspNetCoreModule))]
    public class CommonWebModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddScoped(provider => provider.GetRequiredService<IMapperAccessor>().Mapper);

            context.Services.AddRouting();
            context.Services.AddAuthorization();

            context.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            context.Services.AddCors();
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseAbpClaimsMap();

            app.UseCors(options => options
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
        }
    }
}
