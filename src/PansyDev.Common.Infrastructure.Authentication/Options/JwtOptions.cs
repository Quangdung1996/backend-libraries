namespace PansyDev.Common.Infrastructure.Authentication.Options
{
    public class JwtOptions
    {
        public string Issuer { get; set; } = null!;
        public string Audience { get; set; } = null!;
    }
}