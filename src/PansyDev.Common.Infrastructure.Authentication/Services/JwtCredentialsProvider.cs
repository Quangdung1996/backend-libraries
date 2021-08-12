using Microsoft.IdentityModel.Tokens;
using PansyDev.Common.Infrastructure.Authentication.Options;
using PansyDev.Common.Infrastructure.Authentication.Services.Abstractions;
using PansyDev.Common.Infrastructure.Utils;
using StackExchange.Redis;

namespace PansyDev.Common.Infrastructure.Authentication.Services
{
    internal class JwtCredentialsProvider : IJwtCredentialsProvider
    {
        public JwtCredentialsProvider(IDatabase database, RedisOptions redisOptions)
        {
            var keyValue = database.StringGet(redisOptions.SecurityKeyEntry);

            if (!keyValue.HasValue)
            {
                keyValue = CryptoUtils.GenerateBytes(64);
                database.StringSet(redisOptions.SecurityKeyEntry, keyValue);
            }

            SecurityKey = new SymmetricSecurityKey(keyValue);
            SigningCredentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);
        }

        public SecurityKey SecurityKey { get; }
        public SigningCredentials SigningCredentials { get; }
    }
}
