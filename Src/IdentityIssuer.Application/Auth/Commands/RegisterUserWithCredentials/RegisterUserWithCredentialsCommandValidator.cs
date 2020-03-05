using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using IdentityIssuer.Application.Auth.Repositories;
using IdentityIssuer.Common.Constants;

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
                .NotEmpty();
            RuleFor(x => x.Password)
                .Length(UserConstants.MinPasswordLength, UserConstants.MaxPasswordLength)
                .NotEmpty();
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .Length(UserConstants.MinEmailLength, UserConstants.MaxPasswordLength);
            RuleFor(x => x.Tenant)
                .IsValid();
            RuleFor(x => x)
                .MustAsync(HaveUniqueEmailForTenant)
                .WithMessage(ValidationErrors.EmailNotUniqueForTenant)
                .OverridePropertyName(nameof(RegisterUserWithCredentialsCommand.Email));
        }

        private async Task<bool> HaveUniqueEmailForTenant(RegisterUserWithCredentialsCommand command,
            CancellationToken cancellationToken)
        {
            return !(await _authRepository.IsEmailRegisteredForTenant(command.Email, command.Tenant.TenantId));
        }
    }
}