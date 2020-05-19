using System.Data;
using FluentValidation;
using IdentityIssuer.Common.Enums;

namespace IdentityIssuer.Application.Tenants.Commands.DeleteTenantProviderSettings
{
    public class DeleteTenantProviderSettingsCommandValidator : AbstractValidator<DeleteTenantProviderSettingsCommand>
    {
        public DeleteTenantProviderSettingsCommandValidator()
        {
            RuleFor(x => x.TenantCode)
                .NotEmpty()
                .WithMessageCode(ValidationErrorCode.TenantCodeRequired);

            RuleFor(x => x.ProviderType)
                .IsInEnum()
                .WithMessageCode(ValidationErrorCode.AuthProviderTypeRequired)
                .NotEmpty()
                .WithMessageCode(ValidationErrorCode.AuthProviderTypeRequired);

            RuleFor(x => x.RequestContext)
                .IsInAdminContext();
        }
    }
}