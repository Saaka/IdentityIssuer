using System;
using System.Threading;
using System.Threading.Tasks;
using IdentityIssuer.Common.Exceptions;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace IdentityIssuer.Application.Behaviors
{
    public class CommandLogger<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>, ICommandBase
    {
        private readonly ILogger _logger;

        public CommandLogger(ILogger<TRequest> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            var name = typeof(TRequest).Name;
            TResponse response;

            _logger.LogInformation("{Name} {CommandUuid} handler started", name, request.CommandUuid);
            try
            {
                response = await next();
            }
            catch (CommandValidationException)
            {
                _logger.LogWarning("{Name} {CommandUuid} handler validation failed", name, request.CommandUuid);
                throw;
            }
            catch
            {
                _logger.LogError("{Name} {CommandUuid} handler failed", name, request.CommandUuid);
                throw;
            }

            _logger.LogInformation("{Name} {CommandUuid} handler finished", name, request.CommandUuid);

            return response;
        }
    }
}