using FluentValidation;
using IdentityIssuer.Common.Constants;
using IdentityIssuer.Common.Enums;

namespace IdentityIssuer.Application.Tenants.Commands.ApplyForTenant
{
    public class ApplyForTenantCommandValidator : AbstractValidator<ApplyForTenantCommand>
    {
        public ApplyForTenantCommandValidator()
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

            RuleFor(x => x)
                .Must(HaveOneLoginOptionEnabled)
                .WithName(nameof(UpdateTenantSettingsCommand.EnableCredentialsLogin))
                .WithMessageCode(ValidationErrorCode.OneLoginOptionRequired);

            RuleFor(x => x.OwnerEmail)
                .NotEmpty()
                .WithMessageCode(ValidationErrorCode.UserEmailRequired)
                .EmailAddress()
                .WithMessageCode(ValidationErrorCode.UserEmailInvalid)
                .Length(UserConstants.MinEmailLength, UserConstants.MaxEmailLength)
                .WithMessageCode(ValidationErrorCode.UserEmailInvalid);

            RuleFor(x => x.RequestContext)
                .IsInAdminTenantContext();
        }

        private static bool HaveOneLoginOptionEnabled(ApplyForTenantCommand settings)
        {
            return settings.EnableCredentialsLogin || settings.EnableFacebookLogin || settings.EnableGoogleLogin;
        }
    }
}