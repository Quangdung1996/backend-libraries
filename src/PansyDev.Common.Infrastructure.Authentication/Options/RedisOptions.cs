namespace PansyDev.Common.Infrastructure.Authentication.Options
{
    public class RedisOptions
    {
        public string SecurityKeyEntry { get; set; } = "sk";
        public string InvalidationTimeEntryPrefix { get; set; } = "it:";
    }
}
