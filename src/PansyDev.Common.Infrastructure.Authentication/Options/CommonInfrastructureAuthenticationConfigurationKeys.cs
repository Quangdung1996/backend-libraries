namespace PansyDev.Common.Infrastructure.Authentication.Options
{
    public static class CommonInfrastructureAuthenticationConfigurationKeys
    {
        private const string Prefix = "Common:";

        public const string Redis = Prefix + nameof(Redis);
        public const string Jwt = Prefix + nameof(Jwt);
        public const string Security = Prefix + nameof(Security);
    }
}
