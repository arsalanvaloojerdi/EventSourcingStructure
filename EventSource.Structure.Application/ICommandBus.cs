using Framework.Application.Contracts;
using System.Threading.Tasks;

namespace Framework.Application
{
    public interface ICommandBus
    {
        Task DispatchAsync<T>(T command) where T : ICommandBase;
    }
}