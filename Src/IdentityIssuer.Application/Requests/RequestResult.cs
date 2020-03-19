using IdentityIssuer.Common.Enums;

namespace IdentityIssuer.Application.Requests
{
    public class RequestResult
    {
        public ExceptionCode Error { get; }
        public bool IsSuccess { get; }

        public RequestResult()
            => IsSuccess = true;

        public RequestResult(ExceptionCode error)
            => (IsSuccess, Error) = (false, error);

        public static RequestResult Success()
            => new RequestResult();

        public static RequestResult Failure(ExceptionCode error)
            => new RequestResult(error);
    }

    public class RequestResult<TResult> : RequestResult
    {
        public TResult Data { get; }

        public RequestResult(TResult data)
            => Data = data;

        public RequestResult(ExceptionCode error) : base(error)
        {
        }

        public static RequestResult<TResult> Success(TResult data)
            => new RequestResult<TResult>(data);

        public static RequestResult<TResult> Failure(ExceptionCode error)
            => new RequestResult<TResult>(error);
    }
}