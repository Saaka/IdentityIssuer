using FluentValidation;
using IdentityIssuer.Application.Models;

namespace IdentityIssuer.Application.Validators.FluentValidation
{
    public class UserContextDataValidator : AbstractValidator<UserContextData>
    {
        public UserContextDataValidator()
        {
            RuleFor(x => x)
                .NotNull();
            RuleFor(x => x.UserId)
                .NotEmpty();
            RuleFor(x => x.UserGuid)
                .NotEmpty();
            RuleFor(x => x.Tenant)
                .IsValid();
        }
    }
}