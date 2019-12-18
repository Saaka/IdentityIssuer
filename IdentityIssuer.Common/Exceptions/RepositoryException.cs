using System;
using System.Collections.Generic;
using System.Linq;

namespace IdentityIssuer.Common.Exceptions
{
    public class RepositoryException : Exception
    {
        public RepositoryException(string error)
            : this(error, new List<string> { error })
        {
        }
        
        public RepositoryException(string error, IEnumerable<string> details)
            : base(error)
        {
            Errors = details.ToList();
        }

        public IReadOnlyList<string> Errors { get; private set; }
    }
}