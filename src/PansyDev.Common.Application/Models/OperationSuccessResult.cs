namespace PansyDev.Common.Application.Models
{
    public class OperationSuccessResult<T> : OperationResult<T>
    {
        public OperationSuccessResult(T result)
        {
            Result = result;
        }

        public T Result { get; }
    }
}