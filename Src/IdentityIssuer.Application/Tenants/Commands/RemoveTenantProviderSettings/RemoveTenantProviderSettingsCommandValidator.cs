using FluentValidation;
using IdentityIssuer.Common.Enums;

namespace IdentityIssuer.Application.Tenants.Commands.RemoveTenantProviderSettings
{
    public class RemoveTenantProviderSettingsCommandValidator : AbstractValidator<RemoveTenantProviderSettingsCommand>
    {
        public RemoveTenantProviderSettingsCommandValidator()
        {
            RuleFor(x => x.TenantCode)
                .NotEmpty()
                .WithMessageCode(ValidationErrorCode.TenantCodeRequired);

            RuleFor(x => x.ProviderType)
                .IsInEnum()
                .WithMessageCode(ValidationErrorCode.AuthProviderTypeRequired)
                .NotEmpty()
                .WithMessageCode(ValidationErrorCode.AuthProviderTypeRequired);

            RuleFor(x => x.AdminContextData)
                .IsValid();
        }
    }
}