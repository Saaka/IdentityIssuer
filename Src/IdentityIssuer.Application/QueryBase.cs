using System;
using MediatR;

namespace IdentityIssuer.Application
{
    public interface IQueryBase
    {
        Guid QueryUuid { get; }
    }
    public class QueryBase<T> :  IRequest<T>, IQueryBase
    {
        protected QueryBase()
        {
            QueryUuid = Guid.NewGuid();
        }

        public Guid QueryUuid { get; }
    }
}