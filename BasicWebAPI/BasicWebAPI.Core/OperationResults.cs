using System;

namespace BasicWebAPI.Core
{
    public class OperationResult<TResult> where TResult : class
    {
        private OperationResult() { }

        public bool Success { get; private set; }
        public TResult ResultData { get; private set; }
        public string ErrorMessage { get; private set; }

        public static OperationResult<TResult> CreateSuccessResult(TResult result)
        {
            return new OperationResult<TResult> { Success = true, ResultData = result };
        }

        public static OperationResult<TResult> CreateFailure(Exception exception)
        {
            return new OperationResult<TResult>
            {
                Success = false,
                ErrorMessage = exception.Message
            };
        }
    }
}
