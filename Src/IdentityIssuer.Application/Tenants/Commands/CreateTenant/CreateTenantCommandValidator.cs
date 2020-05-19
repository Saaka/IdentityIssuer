using FluentValidation;
using IdentityIssuer.Common.Constants;
using IdentityIssuer.Common.Enums;

namespace IdentityIssuer.Application.Tenants.Commands.CreateTenant
{
    public class CreateTenantCommandValidator: AbstractValidator<CreateTenantCommand>
    {
        public CreateTenantCommandValidator()
        {
            RuleFor(x => x.TenantCode)
                .NotEmpty()
                .WithMessageCode(ValidationErrorCode.TenantCodeRequired)
                .Length(TenantConstants.TenantCodeMaxLength, TenantConstants.TenantCodeMaxLength)
                .WithMessageCode(ValidationErrorCode.TenantCodeInvalid);

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessageCode(ValidationErrorCode.TenantNameRequired)
                .MaximumLength(TenantConstants.TenantNameMaxLength)
                .WithMessageCode(ValidationErrorCode.TenantNameInvalid);

            RuleFor(x => x.AllowedOrigin)
                .NotEmpty()
                .WithMessageCode(ValidationErrorCode.TenantAllowedOriginRequired);

            RuleFor(x => x.TokenExpirationInMinutes)
                .NotEmpty()
                .WithMessageCode(ValidationErrorCode.TokenExpirationInMinutesRequired);

            RuleFor(x => x.TokenSecret)
                .NotEmpty()
                .WithMessageCode(ValidationErrorCode.TokenSecretRequired)
                .MaximumLength(TenantConstants.TokenSecretMaxLength)
                .WithMessageCode(ValidationErrorCode.TokenSecretInvalid);

            RuleFor(x => x.RequestContext)
                .IsInAdminContext();

            RuleFor(x => x)
                .Must(HaveOneLoginOptionEnabled)
                .WithName(nameof(UpdateTenantSettingsCommand.EnableCredentialsLogin))
                .WithMessageCode(ValidationErrorCode.OneLoginOptionRequired);
        }

        private static bool HaveOneLoginOptionEnabled(CreateTenantCommand settings)
        {
            return settings.EnableCredentialsLogin || settings.EnableFacebookLogin || settings.EnableGoogleLogin;
        }
    }
}