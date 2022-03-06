using System;

namespace Domain.Common.Exceptions
{
    /// <summary>
    /// An exception thrown by the domain when validation of a model fails.
    /// </summary>
    public class DomainValidationException : Exception
    {
        public DomainValidationException(string message) : base(message) { }
    }
}
