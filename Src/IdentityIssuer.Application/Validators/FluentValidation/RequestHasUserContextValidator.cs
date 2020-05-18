using FluentValidation;
using IdentityIssuer.Common.Enums;
using IdentityIssuer.Common.Requests.RequestContext;

namespace IdentityIssuer.Application.Validators.FluentValidation
{
    public class RequestHasUserContextValidator : AbstractValidator<RequestContextData>
    {
        public RequestHasUserContextValidator()
        {
            RuleFor(x => x)
                .HasTenantContext();
            RuleFor(x => x.User)
                .NotNull()
                .WithMessageCode(ValidationErrorCode.UserContextRequired);
            RuleFor(x => x.User.UserId)
                .NotEmpty()
                .WithMessageCode(ValidationErrorCode.UserContextRequired);
            RuleFor(x => x.User.UserGuid)
                .NotEmpty()
                .WithMessageCode(ValidationErrorCode.UserContextRequired);
        }
    }
}