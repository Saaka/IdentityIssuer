using FluentValidation;
using IdentityIssuer.Common.Enums;
using IdentityIssuer.Common.Requests.RequestContexts;

namespace IdentityIssuer.Application.Validators.FluentValidation
{
    public class UserContextValidator : AbstractValidator<UserContext>
    {
        public UserContextValidator()
        {
            RuleFor(x => x)
                .NotNull()
                .WithMessageCode(ValidationErrorCode.UserContextRequired);
            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessageCode(ValidationErrorCode.UserContextRequired);
            RuleFor(x => x.UserGuid)
                .NotEmpty()
                .WithMessageCode(ValidationErrorCode.UserContextRequired);
        }
    }
}