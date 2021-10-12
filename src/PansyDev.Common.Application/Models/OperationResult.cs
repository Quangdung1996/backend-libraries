using Volo.Abp;

namespace PansyDev.Common.Application.Models
{
    public class OperationResult<T>
    {
        public OperationResult(bool success)
        {
            Success = success;
        }

        public bool Success { get; }

        public static implicit operator OperationResult<T>(OperationFailureResult result) =>
            new OperationFailureResult<T>(result.Code);

        public static implicit operator OperationResult<T>(T value) => OperationResult.FromResult(value);
    }

    public class OperationResult
    {
        public OperationResult(bool success)
        {
            Success = success;
        }

        public bool Success { get; }

        public static OperationSuccessResult<T> FromResult<T>(T value) => new(value);
        public static OperationSuccessResult NoContent { get; } = new();

        public static OperationFailureResult FromErrorCode(string errorCode) => new(errorCode);
        public static OperationFailureResult FromBusinessException(BusinessException exception) => new(exception.Code);
    }
}
