namespace IdentityIssuer.Application.Requests
{
    public class RequestResultBase
    {
        public RequestResultBase(string error)
        {
            IsSuccessful = false;
            Error = error;
        }

        public RequestResultBase()
        {
            IsSuccessful = true;
        }

        public bool IsSuccessful { get; set; }
        public string Error { get; set; }
    }
}