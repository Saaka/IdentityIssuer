using FluentValidation;
using IdentityIssuer.Common.Enums;
using IdentityIssuer.Common.Requests.RequestContext;

namespace IdentityIssuer.Application.Validators.FluentValidation
{
    public class IsInAdminContextValidator : AbstractValidator<RequestContextData>
    {
        public IsInAdminContextValidator()
        {
            When(x => x.IsUserContext, () =>
            {
                RuleFor(x => x)
                    .IsInUserContext();
                RuleFor(x => x.User)
                    .SetValidator(new AdminContextValidator());
            });
            When(x => !x.IsUserContext, () =>
            {
                RuleFor(x => x.AdminContextType)
                    .Equal(AdminContextType.System)
                    .WithMessageCode(ValidationErrorCode.AdminContextRequired);
            });
        }
    }
}