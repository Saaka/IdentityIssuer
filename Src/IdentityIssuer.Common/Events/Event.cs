using System;
using MediatR;

namespace IdentityIssuer.Common.Events
{
    public interface IEventBase
    {
        Guid EventGuid { get; }
        Guid CorrelationId { get; }
    }
    public class Event : INotification, IEventBase
    {
        public Guid EventGuid { get; private set; }
        public Guid CorrelationId { get; private set; }
        
        protected Event()
        {
            EventGuid = Guid.NewGuid();
            CorrelationId = Guid.NewGuid();
        }
        public Event WithCorrelationId(Guid guid)
        {
            CorrelationId = guid;
            return this;
        }
    }
}