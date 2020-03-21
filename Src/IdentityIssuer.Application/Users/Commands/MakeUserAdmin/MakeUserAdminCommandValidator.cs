using System.Data;
using FluentValidation;

namespace IdentityIssuer.Application.Users.Commands.MakeUserAdmin
{
    public class MakeUserAdminCommandValidator : AbstractValidator<MakeUserAdminCommand>
    {
        public MakeUserAdminCommandValidator()
        {
            RuleFor(x => x.UserGuid)
                .NotEmpty();
            RuleFor(x => x.AdminContextData)
                .IsValid();
        }
    }
}