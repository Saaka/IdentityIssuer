using FluentValidation;
using IdentityIssuer.Common.Constants;
using IdentityIssuer.Common.Enums;

namespace IdentityIssuer.Application.Tenants.Commands.UpdateTenantSettings
{
    public class UpdateTenantSettingsCommandValidator : AbstractValidator<UpdateTenantSettingsCommand>
    {
        public UpdateTenantSettingsCommandValidator()
        {
            RuleFor(x => x.TenantCode)
                .NotEmpty()
                .WithMessageCode(ValidationErrorCode.TenantCodeRequired);

            RuleFor(x => x.TokenExpirationInMinutes)
                .NotEmpty()
                .WithMessageCode(ValidationErrorCode.TokenExpirationInMinutesRequired);

            RuleFor(x => x.TokenSecret)
                .NotEmpty()
                .WithMessageCode(ValidationErrorCode.TokenSecretRequired)
                .MaximumLength(TenantConstants.TokenSecretMaxLength)
                .WithMessageCode(ValidationErrorCode.TokenSecretInvalid);

            RuleFor(x => x)
                .Must(HaveOneLoginOptionEnabled)
                .WithName(nameof(UpdateTenantSettingsCommand.EnableCredentialsLogin))
                .WithMessageCode(ValidationErrorCode.OneLoginOptionRequired);

            RuleFor(x => x.AllowedOrigins)
                .NotEmpty()
                .WithMessageCode(ValidationErrorCode.TenantAllowedOriginRequired);

            RuleForEach(x => x.AllowedOrigins)
                .NotEmpty()
                .WithMessageCode(ValidationErrorCode.TenantAllowedOriginRequired)
                .WithName(nameof(UpdateTenantSettingsCommand.AllowedOrigins));

            RuleFor(x => x.AdminContextData)
                .IsValid();
        }

        private static bool HaveOneLoginOptionEnabled(UpdateTenantSettingsCommand settings)
        {
            return settings.EnableCredentialsLogin || settings.EnableFacebookLogin || settings.EnableGoogleLogin;
        }
    }
}