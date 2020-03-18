using FluentValidation;
using IdentityIssuer.Common.Constants;

namespace IdentityIssuer.Application.Tenants.Commands.CreateTenant
{
    public class CreateTenantCommandValidator: AbstractValidator<CreateTenantCommand>
    {
        public CreateTenantCommandValidator()
        {
            RuleFor(x => x.Code)
                .NotEmpty()
                .MaximumLength(ValidationConstants.TenantCodeMaxLength)
                .MinimumLength(ValidationConstants.TenantCodeMaxLength);

            RuleFor(x => x.AdminEmail)
                .EmailAddress()
                .NotEmpty()
                .MaximumLength(UserConstants.MaxEmailLength)
                .MinimumLength(UserConstants.MinEmailLength);
            
            RuleFor(x=> x.AdminPassword)
                .NotEmpty()
                .MaximumLength(UserConstants.MaxPasswordLength)
                .MinimumLength(UserConstants.MinPasswordLength);

            RuleFor(x => x.AllowedOrigin)
                .NotEmpty();

            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(ValidationConstants.TenantNameMaxLength);
        }
    }
}