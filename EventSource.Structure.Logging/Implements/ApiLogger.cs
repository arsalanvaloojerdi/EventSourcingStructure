using System;
using System.Threading.Tasks;
using EventSource.Structure.Infrastructure.Logging.Context;
using EventSource.Structure.Infrastructure.Logging.Interfaces;
using EventSource.Structure.Infrastructure.Logging.Models;

namespace EventSource.Structure.Infrastructure.Logging.Implements
{
    internal class ApiLogger : IApiLogger
    {
        private readonly LogContext _context;

        public ApiLogger(LogContext context)
        {
            _context = context;
        }

        public async Task LogException(string controllerName, string actionName, Exception exception)
        {
            var log = new ApiExceptionLog(controllerName, actionName, exception);

            _context.ApiExceptionsLog.Add(log);

            await _context.SaveChangesAsync();
        }
    }
}