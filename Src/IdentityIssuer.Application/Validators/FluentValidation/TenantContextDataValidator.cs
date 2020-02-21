using FluentValidation;
using IdentityIssuer.Application.Models;

namespace IdentityIssuer.Application.Validators.FluentValidation
{
    public class TenantContextDataValidator : AbstractValidator<TenantContextData>
    {
        public TenantContextDataValidator()
        {
            RuleFor(x => x)
                .NotNull();
            RuleFor(x => x.TenantCode)
                .NotEmpty();
            RuleFor(x=> x.TenantId)
                .NotEmpty();
        }
    }
}