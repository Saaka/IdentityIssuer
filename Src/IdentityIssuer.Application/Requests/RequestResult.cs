using IdentityIssuer.Common.Enums;

namespace IdentityIssuer.Application.Requests
{
    public class RequestResult
    {
        public ErrorCode Error { get; }
        public object ErrorDetails { get; }
        public bool IsSuccess { get; }

        public RequestResult()
            => IsSuccess = true;

        public RequestResult(ErrorCode error, object errorDetails = null)
            => (IsSuccess, Error, ErrorDetails) = (false, error, errorDetails);

        public static RequestResult Success()
            => new RequestResult();

        public static RequestResult Failure(ErrorCode error, object errorDetails = null)
            => new RequestResult(error, errorDetails);
    }

    public class RequestResult<TResult> : RequestResult
    {
        public TResult Data { get; }

        public RequestResult(TResult data)
            => Data = data;

        public RequestResult(ErrorCode error, object errorDetails = null) : base(error, errorDetails)
        {
        }

        public static RequestResult<TResult> Success(TResult data)
            => new RequestResult<TResult>(data);

        public new static RequestResult<TResult> Failure(ErrorCode error, object errorDetails = null)
            => new RequestResult<TResult>(error, errorDetails);
    }
}