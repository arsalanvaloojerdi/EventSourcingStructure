using System;
using EventSource.Structure.Infrastructure.Logging.Extentions;
using Framework.Application.Contracts;

namespace EventSource.Structure.Infrastructure.Logging.Models
{
    public class CommandLog : BaseLog
    {
        private CommandLog(ICommandBase command, Exception exception, bool operationSuccess)
            : base(exception, operationSuccess)
        {
            this.SerializedCommand = command.ToSerialize();
            this.CommandType = command.GetType().Name;
        }

        public static CommandLog Success(ICommandBase command)
        {
            return new CommandLog(command, null, true);
        }

        public static CommandLog Fail(ICommandBase command, Exception exception)
        {
            return new CommandLog(command, exception, false);
        }

        /// <summary>
        /// دیتای موجود در کامند
        /// </summary>
        public string SerializedCommand { get; set; }

        /// <summary>
        /// نوع کامند
        /// </summary>
        public string CommandType { get; set; }

        // FOR ORM !
        private CommandLog() { }
    }
}