using System;
using System.Threading.Tasks;

namespace Framework.Application.Contracts
{
    public interface ICommandLogger
    {
        Task LogAsync(ICommandBase command, Exception exception);
    }
}