using System.Threading;
using System.Threading.Tasks;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace IdentityIssuer.Application.Behaviors
{
    public class CommandLogger<TCommand> : IRequestPreProcessor<TCommand>
    {
        private readonly ILogger _logger;

        public CommandLogger(ILogger<TCommand> logger)
        {
            _logger = logger;
        }

        public Task Process(TCommand command, CancellationToken cancellationToken)
        {
            var name = typeof(TCommand).Name;
            
            _logger.LogInformation("***IdentityIssuer*** Command: {Name} handler started", name);

            return Task.CompletedTask;
        }
    }
}