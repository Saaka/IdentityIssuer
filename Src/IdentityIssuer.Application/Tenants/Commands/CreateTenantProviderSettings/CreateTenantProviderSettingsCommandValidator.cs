using System.Data;
using FluentValidation;
using IdentityIssuer.Common.Constants;
using IdentityIssuer.Common.Enums;

namespace IdentityIssuer.Application.Tenants.Commands.CreateTenantProviderSettings
{
    public class CreateTenantProviderSettingsCommandValidator : AbstractValidator<CreateTenantProviderSettingsCommand>
    {
        public CreateTenantProviderSettingsCommandValidator()
        {
            RuleFor(x => x.TenantCode)
                .NotEmpty()
                .WithMessageCode(ValidationErrorCode.TenantCodeRequired);

            RuleFor(x => x.ProviderType)
                .IsInEnum()
                .WithMessageCode(ValidationErrorCode.AuthProviderTypeRequired)
                .NotEmpty()
                .WithMessageCode(ValidationErrorCode.AuthProviderTypeRequired);

            RuleFor(x => x.Identifier)
                .NotEmpty()
                .WithMessageCode(ValidationErrorCode.AuthProviderIdentifierRequired)
                .MaximumLength(TenantConstants.ProviderIdentifierMaxLength)
                .WithMessageCode(ValidationErrorCode.AuthProviderIdentifierTooLong);
            
            RuleFor(x => x.Key)
                .NotEmpty()
                .WithMessageCode(ValidationErrorCode.AuthProviderKeyRequired)
                .MaximumLength(TenantConstants.ProviderKeyMaxLength)
                .WithMessageCode(ValidationErrorCode.AuthProviderKeyTooLong);

            RuleFor(x => x.AdminContextData)
                .IsValid();
        }
    }
}