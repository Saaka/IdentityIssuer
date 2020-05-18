using FluentValidation;
using IdentityIssuer.Common.Requests.RequestContext;

namespace IdentityIssuer.Application.Validators.FluentValidation
{
    public class IsInAdminTenantContextValidator : AbstractValidator<RequestContextData>
    {
        public IsInAdminTenantContextValidator()
        {
            RuleFor(x => x.Tenant)
                .SetValidator(new AdminTenantContextValidator());
        }
    }
}