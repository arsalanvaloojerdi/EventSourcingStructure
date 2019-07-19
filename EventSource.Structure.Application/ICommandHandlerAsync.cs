using System.Threading.Tasks;
using Framework.Application.Contracts;

namespace Framework.Application
{
    public interface ICommandHandler { }

    public interface ICommandHandlerAsync<in T> where T : ICommandBase
    {
        Task HandleAsync(T command);
    }
}