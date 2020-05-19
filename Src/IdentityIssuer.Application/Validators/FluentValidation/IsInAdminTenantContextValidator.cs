using FluentValidation;
using IdentityIssuer.Common.Requests.RequestContexts;

namespace IdentityIssuer.Application.Validators.FluentValidation
{
    public class IsInAdminTenantContextValidator : AbstractValidator<RequestContext>
    {
        public IsInAdminTenantContextValidator()
        {
            RuleFor(x => x)
                .IsInTenantContext();
            RuleFor(x => x.Tenant)
                .SetValidator(new AdminTenantContextValidator());
        }
    }
}