using System;

namespace Framework.Core.Exceptions.Exceptions.CommandHandler
{
    public class CommandValidationException : Exception
    {
        /// <inheritdoc />
        public CommandValidationException()
        {
        }

        /// <inheritdoc />
        public CommandValidationException(string message) : base(message)
        {
        }
    }
}