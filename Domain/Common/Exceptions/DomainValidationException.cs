using System;

namespace Domain.Common.Exceptions
{
    public class DomainValidationException : Exception
    {
        public DomainValidationException(string message) : base(message) { }
    }
}
