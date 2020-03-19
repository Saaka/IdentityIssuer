namespace IdentityIssuer.Application.Requests
{
    public class RequestResult
    {
        public RequestResult(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }
        public bool IsSuccess { get; set; }
    }
    
    public class RequestResult<TResult> : RequestResult
    {
        public RequestResult(TResult data) : base(true)
        {
            Data = data;
        }
        
        public TResult Data { get; }

    }
}