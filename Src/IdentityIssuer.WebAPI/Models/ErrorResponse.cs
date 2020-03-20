using IdentityIssuer.Common.Enums;

namespace IdentityIssuer.WebAPI.Models
{
    public class ErrorResponse
    {
        public string Error => ErrorCode.ToString();
        public ErrorCode ErrorCode { get; }
        public string ErrorDetails { get; }

        public ErrorResponse(ErrorCode code, string details = null)
            => (ErrorCode, ErrorDetails) = (code, details);
    }
}