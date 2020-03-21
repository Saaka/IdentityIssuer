using System;
using System.Threading;
using System.Threading.Tasks;
using IdentityIssuer.Application.Users.Repositories;
using IdentityIssuer.Common.Enums;
using IdentityIssuer.Common.Requests;

namespace IdentityIssuer.Application.Users.Commands.MakeUserAdmin
{
    public class MakeUserAdminCommandHandler : RequestHandler<MakeUserAdminCommand, Guid>
    {
        private readonly IUserRepository _userRepository;

        public MakeUserAdminCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public override async Task<RequestResult<Guid>> Handle(MakeUserAdminCommand request,
            CancellationToken cancellationToken)
        {
            if (!await _userRepository.UserExistsAsync(request.UserGuid))
                return RequestResult.Failure(ErrorCode.UserNotFound, new {userGuid = request.UserGuid});

            if (await _userRepository.SetUserAdminValue(request.UserGuid, true))
                return RequestResult.Success(request);

            return RequestResult.Failure(ErrorCode.UserUpdateFailed);
        }
    }
}