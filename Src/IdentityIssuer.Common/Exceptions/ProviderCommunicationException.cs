using System;

namespace IdentityIssuer.Common.Exceptions
{
    public class ProviderCommunicationException : Exception
    {
        public ProviderCommunicationException(string message)
            : base(message)
        {
        }
    }
}