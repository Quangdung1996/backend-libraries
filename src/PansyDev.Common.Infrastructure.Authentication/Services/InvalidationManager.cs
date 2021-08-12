using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using PansyDev.Common.Infrastructure.Authentication.Options;
using StackExchange.Redis;
using Volo.Abp.DependencyInjection;

namespace PansyDev.Common.Infrastructure.Authentication.Services
{
    public class InvalidationManager : ISingletonDependency
    {
        private readonly IDatabase _database;
        private readonly SecurityOptions _securityOptions;
        private readonly RedisOptions _redisOptions;

        public InvalidationManager(IDatabase database, IOptions<SecurityOptions> securityOptions,
            IOptions<RedisOptions> coreRedisOptions)
        {
            _database = database;
            _securityOptions = securityOptions.Value;
            _redisOptions = coreRedisOptions.Value;
        }

        public async Task<DateTime?> GetInvalidationDate(Guid sessionId)
        {
            var value = await _database.StringGetAsync(_redisOptions.InvalidationTimeEntryPrefix + sessionId);
            if (!value.HasValue) return null;

            return DateTime.Parse(value);
        }

        public async Task Invalidate(Guid sessionId)
        {
            await _database.StringSetAsync(_redisOptions.InvalidationTimeEntryPrefix + sessionId,
                DateTime.UtcNow.ToString("O"),
                _securityOptions.AccessTokenLifeTime);
        }
    }
}
