using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using IdentityIssuer.Application.Users.Repositories;
using IdentityIssuer.Common.Constants;

namespace IdentityIssuer.Application.Users.Commands.RegisterUserWithCredentials
{
    public class RegisterUserWithCredentialsCommandValidator : AbstractValidator<RegisterUserWithCredentialsCommand>
    {
        private readonly IUserRepository userRepository;

        public RegisterUserWithCredentialsCommandValidator(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
            
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
            RuleFor(x => x.TenantId)
                .NotEmpty();
            RuleFor(x => x)
                .MustAsync(HaveUniqueEmailForTenant)
                .WithMessage(ValidationErrors.EmailNotUniqueForTenant)
                .OverridePropertyName(nameof(RegisterUserWithCredentialsCommand.Email));
        }

        private async Task<bool> HaveUniqueEmailForTenant(RegisterUserWithCredentialsCommand command,
            CancellationToken cancellationToken)
        {
            return await userRepository.IsEmailUniqueForTenant(command.Email, command.TenantId);
        }
    }
}