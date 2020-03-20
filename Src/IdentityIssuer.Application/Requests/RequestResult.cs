using System;
using IdentityIssuer.Common.Enums;

namespace IdentityIssuer.Application.Requests
{
    public abstract class RequestResult
    {
        public ErrorCode Error { get; }
        public object ErrorDetails { get; }
        public bool IsSuccess { get; }

        public RequestResult()
            => IsSuccess = true;

        protected RequestResult(ErrorCode error, object errorDetails = null)
            => (IsSuccess, Error, ErrorDetails) = (false, error, errorDetails);

        /// <summary>
        /// Create Success RequestResult that return Request Guid as value.
        /// </summary>
        /// <param name="request">Base Request object to return Guid from</param>
        /// <returns>RequestResult with Request Guid</returns>
        public static RequestResult<Guid> Success(IRequestBase request)
            => new RequestResult<Guid>(request.RequestGuid);

        /// <summary>
        /// Create Success RequestResult that return Request Guid as value.
        /// </summary>
        /// <param name="requestGuid">Request Guid</param>
        /// <returns>RequestResult with Request Guid</returns>
        public static RequestResult<Guid> Success(Guid requestGuid)
            => new RequestResult<Guid>(requestGuid);

        /// <summary>
        /// Create Failure RequestResult with ErrorCode and optionally error details.
        /// </summary>
        /// <param name="error">ErrorCode that indicates the problem.</param>
        /// <param name="errorDetails">Details of error that occured (optional).</param>
        /// <returns>RequestResult with ErrorCode and optional details</returns>
        public static RequestResult<Guid> Failure(ErrorCode error, object errorDetails = null)
            => new RequestResult<Guid>(error, errorDetails);
    }

    public class RequestResult<TResult> : RequestResult
    {
        public TResult Data { get; }

        public RequestResult(TResult data) : base()
            => Data = data;

        public RequestResult(ErrorCode error, object errorDetails = null) : base(error, errorDetails)
        {
        }

        /// <summary>
        /// Create Failure RequestResult with ErrorCode and optionally error details.
        /// </summary>
        /// <param name="error">ErrorCode that indicates the problem.</param>
        /// <param name="errorDetails">Details of error that occured (optional).</param>
        /// <returns>RequestResult with ErrorCode and optional details</returns>
        public new static RequestResult<TResult> Failure(ErrorCode error, object errorDetails = null)
            => new RequestResult<TResult>(error, errorDetails);

        /// <summary>
        /// Create Success RequestResult that return generic object as value.
        /// </summary>
        /// <param name="data">Generic object value.</param>
        /// <returns>RequestResult with generic value.</returns>
        public static RequestResult<TResult> Success(TResult data)
            => new RequestResult<TResult>(data);
        
        /// <summary>
        /// Create Success RequestResult that return Request Guid as value.
        /// </summary>
        /// <param name="request">Base Request object to return Guid from</param>
        /// <returns>RequestResult with Request Guid</returns>
        public new static RequestResult<Guid> Success(IRequestBase request)
            => new RequestResult<Guid>(request.RequestGuid);

        /// <summary>
        /// Create Success RequestResult that return Request Guid as value.
        /// </summary>
        /// <param name="requestGuid">Request Guid</param>
        /// <returns>RequestResult with Request Guid</returns>
        public new static RequestResult<Guid> Success(Guid requestGuid)
            => new RequestResult<Guid>(requestGuid);
    }
}