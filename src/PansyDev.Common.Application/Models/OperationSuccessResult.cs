namespace PansyDev.Common.Application.Models
{
    public class OperationSuccessResult<T> : OperationResult<T>
    {
        public OperationSuccessResult(T result) : base(true)
        {
            Result = result;
        }

        public T Result { get; }
    }

    public class OperationSuccessResult : OperationResult
    {
        public OperationSuccessResult() : base(true) { }
    }
}
