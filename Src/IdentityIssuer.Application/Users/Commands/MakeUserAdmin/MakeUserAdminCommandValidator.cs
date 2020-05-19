using System.Data;
using FluentValidation;
using IdentityIssuer.Common.Enums;

namespace IdentityIssuer.Application.Users.Commands.MakeUserAdmin
{
    public class MakeUserAdminCommandValidator : AbstractValidator<MakeUserAdminCommand>
    {
        public MakeUserAdminCommandValidator()
        {
            RuleFor(x => x.UserGuid)
                .NotEmpty()
                .WithMessageCode(ValidationErrorCode.UserGuidRequired);
            RuleFor(x => x.RequestContext)
                .IsInAdminContext();
        }
    }
}