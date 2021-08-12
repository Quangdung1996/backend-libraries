namespace PansyDev.Common.Application.Models
{
    public class OperationFailureResult<T> : OperationResult<T>, IOperationFailureResult
    {
        public OperationFailureResult(string code)
        {
            Code = code;
        }

        public string Code { get; }
    }

    public class OperationFailureResult : OperationResult, IOperationFailureResult
    {
        public OperationFailureResult(string code)
        {
            Code = code;
        }

        public string Code { get; }
    }

    public interface IOperationFailureResult
    {
        public string Code { get; }
    }
}
