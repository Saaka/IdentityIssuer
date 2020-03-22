using FluentValidation;
using IdentityIssuer.Application.Models;
using IdentityIssuer.Common.Enums;

namespace IdentityIssuer.Application.Validators.FluentValidation
{
    public class UserContextDataValidator : AbstractValidator<UserContextData>
    {
        public UserContextDataValidator()
        {
            RuleFor(x => x)
                .NotNull()
                .WithMessageCode(ValidationErrorCode.UserContextRequired);
            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessageCode(ValidationErrorCode.UserContextRequired);;
            RuleFor(x => x.UserGuid)
                .NotEmpty()
                .WithMessageCode(ValidationErrorCode.UserContextRequired);;
            RuleFor(x => x.Tenant)
                .IsValid();
        }
    }
}