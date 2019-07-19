using System;

namespace Framework.Core.Exceptions.Exceptions.CommandHandler
{
    public class CommandHandlerException : Exception
    {
        public CommandHandlerException(string message) : base(message)
        {
        }
    }
}