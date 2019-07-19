using Framework.Core;
using System.Diagnostics;
using System.Threading.Tasks;
using Framework.Application.Contracts;

namespace Framework.Application
{
    public class InMemoryCommandBus : ICommandBus
    {
        private readonly IServiceLocator _serviceLocator;

        public InMemoryCommandBus(IServiceLocator serviceLocator)
        {
            _serviceLocator = serviceLocator;
        }

        /// <inheritdoc />
        public async Task DispatchAsync<T>(T command) where T : ICommandBase
        {
            var handler = _serviceLocator.GetInstance<LoggingCommandHandlerDecorator<T>>();
            if (handler == null)
                Debug.Fail("Command Handler Not Found");

            await handler.HandleAsync(command);
        }
    }
}