using System.Threading;
using System.Threading.Tasks;
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
            
            _logger.LogInformation("***IdentityIssuer*** Command: {Name} handler started", name);
            var response = await next();
            _logger.LogInformation("***IdentityIssuer*** Command: {Name} handler finished", name);

            return response;
        }
    }
}