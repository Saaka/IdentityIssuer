using System.Threading;
using System.Threading.Tasks;
using IdentityIssuer.Application.Requests;
using IdentityIssuer.Common.Exceptions;
using Microsoft.Extensions.Logging;

namespace IdentityIssuer.Application.Behaviors
{
    public class RequestLogger<TRequest, TResponse> : MediatR.IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequestBase
    {
        private readonly ILogger _logger;

        public RequestLogger(ILogger<RequestLogger<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            MediatR.RequestHandlerDelegate<TResponse> next)
        {
            var name = typeof(TRequest).Name;
            TResponse response;

            _logger.LogInformation("{Name} handler started. [{RequestGuid}]", name, request.RequestGuid);
            try
            {
                response = await next();
            }
            catch (CommandValidationException)
            {
                _logger.LogWarning("{Name} handler validation failed. [{RequestGuid}]", name, request.RequestGuid);
                throw;
            }
            catch
            {
                _logger.LogError("{Name} handler failed with exception. [{RequestGuid}]", name, request.RequestGuid);
                throw;
            }

            _logger.LogInformation("{Name} handler finished. [{RequestGuid}]", name, request.RequestGuid);

            return response;
        }
    }
}