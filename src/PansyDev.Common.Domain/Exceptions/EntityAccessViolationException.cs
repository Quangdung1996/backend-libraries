using Volo.Abp;

namespace PansyDev.Common.Domain.Exceptions
{
    public class EntityAccessViolationException : BusinessException
    {
        public EntityAccessViolationException() : base(CommonDomainErrorCodes.EntityAccessViolation) { }
    }
}
