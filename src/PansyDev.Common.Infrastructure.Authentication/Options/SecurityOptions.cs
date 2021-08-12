using System;

namespace PansyDev.Common.Infrastructure.Authentication.Options
{
    public class SecurityOptions
    {
        public TimeSpan AccessTokenLifeTime { get; set; }
        public TimeSpan RefreshTokenLifeTime { get; set; }
    }
}
