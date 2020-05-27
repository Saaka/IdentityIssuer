using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace IdentityIssuer.Application.Auth.Events.SendEmailOnUserRegistered
{
    public class SendEmailOnUserRegisteredHandler : INotificationHandler<UserRegisteredWithCredentials>
    {
        private readonly IMediator _mediator;
        private readonly ILogger<SendEmailOnUserRegisteredHandler> _logger;

        public SendEmailOnUserRegisteredHandler(IMediator mediator,
            ILogger<SendEmailOnUserRegisteredHandler> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        
        public async Task Handle(UserRegisteredWithCredentials notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Sending email to user{notification.UserGuid}");    
        }
    }
}