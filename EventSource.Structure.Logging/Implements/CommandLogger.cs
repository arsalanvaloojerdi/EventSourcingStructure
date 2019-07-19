using EventSource.Structure.Infrastructure.Logging.Context;
using EventSource.Structure.Infrastructure.Logging.Models;
using Framework.Application.Contracts;
using System;
using System.Threading.Tasks;

namespace EventSource.Structure.Infrastructure.Logging.Implements
{
    public class CommandLogger : ICommandLogger
    {
        private readonly LogContext _context;

        public CommandLogger(LogContext context)
        {
            _context = context;
        }

        public async Task LogAsync(ICommandBase command, Exception exception)
        {
            var log = exception == null ? CommandLog.Success(command) : CommandLog.Fail(command, exception);

            _context.CommandLogs.Add(log);

            await _context.SaveChangesAsync();
        }
    }
}