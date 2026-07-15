namespace YouthOpportunities.Application.Common.Results
{
    public class Result
    {
        protected Result(
            bool succeeded,
            Error error)
        {
            Succeeded = succeeded;
            Error = error;
        }

        public bool Succeeded { get; }
        public bool Failed => !Succeeded;
        public Error Error { get; }
        public ResultStatus Status => Error.Status;
        public string ErrorCode => Error.Code;
        public string Message => Error.Message;
        public static Result Success()
        {
            return new Result(
                true,
                Error.None);
        }

        public static Result Failure(
        Error error)
            {
                return new Result(
                    false,
                    error);
            }
        }

    public sealed class Result<T> : Result
    {
        private Result(
            bool succeeded,
            T? data,
            Error error)
            : base(succeeded, error)
        {
            Data = data;
        }
        public T? Data { get; }

        public static Result<T> Success(T data)
        {
            return new Result<T>(
                true,
                data,
                Error.None);
        }

        public new static Result<T> Failure(
            Error error)
        {
            return new Result<T>(
                false,
                default,
                error);
        }
    }
}


