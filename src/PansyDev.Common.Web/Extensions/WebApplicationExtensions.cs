using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace PansyDev.Common.Web.Extensions
{
    public static class WebApplicationExtensions
    {
        public static void UseCommonServices(this WebApplication app)
        {
            app.InitializeApplication();

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
        }
    }
}
