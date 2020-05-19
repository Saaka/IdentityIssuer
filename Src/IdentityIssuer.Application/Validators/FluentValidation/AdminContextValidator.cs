using FluentValidation;
using IdentityIssuer.Common.Enums;
using IdentityIssuer.Common.Requests.RequestContexts;

namespace IdentityIssuer.Application.Validators.FluentValidation
{
    public class AdminContextValidator : AbstractValidator<UserContext>
    {
        public AdminContextValidator()
        {
            RuleFor(x => x.IsAdmin)
                .Equal(true)
                .WithMessageCode(ValidationErrorCode.AdminContextRequired);
        }
    }
}