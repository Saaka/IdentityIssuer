using FluentValidation;
using IdentityIssuer.Common.Requests.RequestContexts;

namespace IdentityIssuer.Application.Validators.FluentValidation
{
    public class IsInUserContextValidator : AbstractValidator<RequestContext>
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