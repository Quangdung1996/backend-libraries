using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PansyDev.Common.Infrastructure.Authentication.Options;
using PansyDev.Common.Infrastructure.Authentication.Services;
using PansyDev.Common.Infrastructure.Authentication.Services.Abstractions;
using PansyDev.Common.Infrastructure.Extensions;
using StackExchange.Redis;
using Volo.Abp.Modularity;

namespace PansyDev.Common.Infrastructure.Authentication
{
    [DependsOn(typeof(CommonInfrastructureModule))]
    public class CommonInfrastructureAuthenticationModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            context.Configure<JwtOptions>(CommonInfrastructureAuthenticationConfigurationKeys.Jwt);
            context.Configure<RedisOptions>(CommonInfrastructureAuthenticationConfigurationKeys.Redis);
            context.Configure<SecurityOptions>(CommonInfrastructureAuthenticationConfigurationKeys.Security);
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();

            var jwtOptions = context.Services.ExecutePreConfiguredActions<JwtOptions>();
            var redisOptions = context.Services.ExecutePreConfiguredActions<RedisOptions>();

            var connection = ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis"));
            context.Services.AddSingleton<IConnectionMultiplexer>(connection);

            var database = connection.GetDatabase();
            context.Services.AddSingleton(database);

            var credentialsProvider = new JwtCredentialsProvider(database, redisOptions);
            context.Services.AddSingleton<IJwtCredentialsProvider>(credentialsProvider);

            void ConfigureJwtOptions(JwtBearerOptions options)
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateLifetime = true,

                    ValidateIssuer = true,
                    ValidIssuer = jwtOptions.Issuer,

                    RequireAudience = true,
                    ValidateAudience = true,
                    ValidAudience = jwtOptions.Audience,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = credentialsProvider.SecurityKey
                };
            }

            context.Services.AddAuthentication(CommonAuthenticationScheme.CustomJwtBearer)
                .AddScheme<JwtBearerOptions, CommonAuthenticationHandler>(
                    CommonAuthenticationScheme.CustomJwtBearer, ConfigureJwtOptions);
        }
    }
}
