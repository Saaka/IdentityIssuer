using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using IdentityIssuer.Application.Auth.Repositories;
using IdentityIssuer.Common.Constants;
using IdentityIssuer.Common.Enums;

namespace IdentityIssuer.Application.Auth.Commands.RegisterUserWithCredentials
{
    public class RegisterUserWithCredentialsCommandValidator : AbstractValidator<RegisterUserWithCredentialsCommand>
    {
        private readonly IAuthRepository _authRepository;

        public RegisterUserWithCredentialsCommandValidator(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
            
            RuleFor(x => x.DisplayName)
                .Length(UserConstants.MinDisplayNameLength, UserConstants.MaxDisplayNameLength)
                .WithMessageCode(ValidationErrorCode.UserNameInvalid)
                .NotEmpty()
                .WithMessageCode(ValidationErrorCode.UserNameRequired);
            RuleFor(x => x.Password)
                .Length(UserConstants.MinPasswordLength, UserConstants.MaxPasswordLength)
                .WithMessageCode(ValidationErrorCode.UserPasswordInvalid)
                .NotEmpty()
                .WithMessageCode(ValidationErrorCode.UserPasswordRequired);
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessageCode(ValidationErrorCode.UserEmailRequired)
                .EmailAddress()
                .WithMessageCode(ValidationErrorCode.UserEmailInvalid)
                .Length(UserConstants.MinEmailLength, UserConstants.MaxEmailLength)
                .WithMessageCode(ValidationErrorCode.UserEmailInvalid);
            RuleFor(x => x.RequestContext)
                .IsInTenantContext();
            RuleFor(x => x)
                .MustAsync(HaveUniqueEmailForTenant)
                .OverridePropertyName(nameof(RegisterUserWithCredentialsCommand.Email))
                .WithMessageCode(ValidationErrorCode.UserEmailNotUnique);
        }

        private async Task<bool> HaveUniqueEmailForTenant(RegisterUserWithCredentialsCommand command,
            CancellationToken cancellationToken)
        {
            return !(await _authRepository
                .IsEmailRegisteredForTenant(command.Email, command.RequestContext.Tenant.TenantId));
        }
    }
}