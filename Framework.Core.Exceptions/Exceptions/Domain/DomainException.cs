using System;

namespace Framework.Core.Exceptions.Exceptions.Domain
{
    public class DomainException : Exception
    {
        /// <inheritdoc />
        public DomainException()
        {
        }

        /// <inheritdoc />
        public DomainException(string message) : base(message)
        {
        }
    }
}