using System;
using System.Threading.Tasks;

namespace EventSource.Structure.Infrastructure.Logging.Interfaces
{
    public interface IApiLogger
    {
        Task LogException(string controllerName, string actionName, Exception exception);
    }
}