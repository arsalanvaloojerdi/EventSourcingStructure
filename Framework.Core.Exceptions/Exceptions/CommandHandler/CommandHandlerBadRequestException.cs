using System;

namespace Framework.Core.Exceptions.Exceptions.CommandHandler
{
    public class CommandHandlerBadRequestException : Exception
    {
        public CommandHandlerBadRequestException(string message) : base(message)
        {
        }
    }
}