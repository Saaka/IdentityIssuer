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
        where TRequest : IRequest<TResponse>
    {
        private readonly ILogger _logger;

        public CommandLogger(ILogger<TRequest> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var name = typeof(TRequest).Name;
            TResponse response;
            
            _logger.LogInformation("***IdentityIssuer*** Command: {Name} handler started", name);
            try
            {
                response = await next();
            }
            catch (CommandValidationException)
            {
                _logger.LogWarning("***IdentityIssuer*** Command: {Name} handler validation failed", name);
                throw;
            }
            catch
            {
                _logger.LogError("***IdentityIssuer*** Command: {Name} handler failed", name);
                throw;
            }
            _logger.LogInformation("***IdentityIssuer*** Command: {Name} handler finished", name);

            return response;
        }
    }
}