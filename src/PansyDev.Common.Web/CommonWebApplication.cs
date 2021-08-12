using Microsoft.AspNetCore.Builder;
using PansyDev.Common.Web.Extensions;
using Volo.Abp.Modularity;

namespace PansyDev.Common.Web
{
    public static class CommonWebApplication
    {
        public static void Run<TWebModule, TInfraModule>(string[] args)
            where TWebModule : class, IAbpModule
            where TInfraModule : class, IAbpModule
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.ConfigureCommonServices<TWebModule, TInfraModule>();

            var app = builder.Build();
            app.UseCommonServices();

            app.Run();
        }
    }
}
