using System.Threading;
using System.Threading.Tasks;
using IdentityIssuer.Common.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace IdentityIssuer.Application.Behaviors
{
    public class CommandLogger<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ILogger logger;

        public CommandLogger(ILogger<TRequest> logger)
        {
            this.logger = logger;
        }

        public async Task<TResponse> Handle(TRequest req, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            if (!(req is ICommandBase request))
                return await next();
            
            var name = typeof(TRequest).Name;
            TResponse response;

            logger.LogInformation("{Name} {CommandUuid} handler started", name, request.CommandUuid);
            try
            {
                response = await next();
            }
            catch (CommandValidationException)
            {
                logger.LogWarning("{Name} {CommandUuid} handler validation failed", name, request.CommandUuid);
                throw;
            }
            catch
            {
                logger.LogError("{Name} {CommandUuid} handler failed", name, request.CommandUuid);
                throw;
            }

            logger.LogInformation("{Name} {CommandUuid} handler finished", name, request.CommandUuid);

            return response;
        }
    }
}