namespace PansyDev.Common.Domain.Exceptions
{
    public static class CommonDomainErrorCodes
    {
        private const string Prefix = "Common:";

        public const string EntityAccessViolation = Prefix + nameof(EntityAccessViolation);
    }
}
