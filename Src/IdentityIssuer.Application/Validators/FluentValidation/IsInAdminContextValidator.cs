using FluentValidation;
using IdentityIssuer.Common.Requests.RequestContext;

namespace IdentityIssuer.Application.Validators.FluentValidation
{
    public class IsInAdminContextValidator : AbstractValidator<RequestContextData>
    {
        public IsInAdminContextValidator()
        {
            RuleFor(x => x)
                .IsInUserContext();
            RuleFor(x => x.User)
                .SetValidator(new AdminContextValidator());
        }
    }
}