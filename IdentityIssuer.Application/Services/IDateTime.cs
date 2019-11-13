using System;

namespace IdentityIssuer.Application.Services
{
    public interface IDateTime
    {
        DateTime GetUtcNow();
    }
}