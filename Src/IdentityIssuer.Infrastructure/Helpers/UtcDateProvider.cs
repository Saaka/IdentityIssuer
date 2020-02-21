using System;
using IdentityIssuer.Application.Services;

namespace IdentityIssuer.Infrastructure.Helpers
{
    public class UtcDateProvider : IDateTime
    {
        public DateTime GetUtcNow() => DateTime.UtcNow;
    }
}