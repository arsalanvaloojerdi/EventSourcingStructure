using System;

namespace Framework.Core.Exceptions.Exceptions.CommandHandler
{
    public class CommandHandlerNotFoundException : Exception
    {
        public CommandHandlerNotFoundException(string message) : base(message)
        {
        }
    }
}