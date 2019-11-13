using System;
using IdentityIssuer.Application.Services;

namespace IdentityIssuer.Infrastructure.Helpers
{
    public class GuidProvider : IGuid
    {
        public Guid GetGuid() => Guid.NewGuid();

        public string GetNormalizedGuid() => Guid.NewGuid().ToString("N");
    }
}