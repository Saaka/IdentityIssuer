using FluentValidation;
using IdentityIssuer.Common.Constants;
using IdentityIssuer.Common.Enums;

namespace IdentityIssuer.Application.Tenants.Commands.UpdateTenantProviderSettings
{
    public class UpdateTenantProviderSettingsCommandValidator : AbstractValidator<UpdateTenantProviderSettingsCommand>
    {
        public UpdateTenantProviderSettingsCommandValidator()
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

            RuleFor(x => x.RequestContext)
                .IsInAdminContext();
        }
    }
}