using Framework.Application.Contracts;
using System;
using System.Threading.Tasks;
using Framework.Core.Exceptions.Exceptions.CommandHandler;
using Framework.Core.Exceptions.Exceptions.Domain;

namespace Framework.Application
{
    public class LoggingCommandHandlerDecorator<T> : ICommandHandlerAsync<T> where T : ICommandBase
    {
        private readonly ICommandHandlerAsync<T> _decoratedCommandHandler;
        private readonly ICommandLogger _commandLogger;

        public LoggingCommandHandlerDecorator(
            ICommandHandlerAsync<T> decoratedCommandHandler,
            ICommandLogger commandLogger)
        {
            _decoratedCommandHandler = decoratedCommandHandler;
            _commandLogger = commandLogger;
        }

        /// <inheritdoc />
        public async Task HandleAsync(T command)
        {
            try
            {
                await _decoratedCommandHandler.HandleAsync(command);

                await _commandLogger.LogAsync(command, null);
            }
            catch (DomainException exception)
            {
                throw new CommandHandlerBadRequestException(exception.Message);
            }
            catch (CommandValidationException exception)
            {
                throw new CommandHandlerBadRequestException(exception.Message);
            }
            catch (Exception exception)
            {
                await _commandLogger.LogAsync(command, exception);

                throw new CommandHandlerException(exception.Message);
            }
        }
    }
}