using Microsoft.IdentityModel.Tokens;

namespace PansyDev.Common.Infrastructure.Authentication.Services.Abstractions
{
    public interface IJwtCredentialsProvider
    {
        SecurityKey SecurityKey { get; }
        SigningCredentials SigningCredentials { get; }
    }
}
