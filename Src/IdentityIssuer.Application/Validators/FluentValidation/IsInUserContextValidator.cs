using FluentValidation;
using IdentityIssuer.Common.Enums;
using IdentityIssuer.Common.Requests.RequestContext;

namespace IdentityIssuer.Application.Validators.FluentValidation
{
    public class IsInUserContextValidator : AbstractValidator<RequestContextData>
    {
        public IsInUserContextValidator()
        {
            RuleFor(x => x)
                .IsInTenantContext();
            RuleFor(x => x.User)
                .SetValidator(new UserContextValidator());
        }
    }
}