using Framework.Core;
using Framework.DomainModel.Events;
using System;
using System.Threading.Tasks;

namespace Framework.QueryModel.Synchronizer
{
	public interface IMediator
	{
		Task SendAsync<T>(T @event, Type projectionType) where T : IDomainEvent;
	}

	public class WindsorMediator : IMediator
	{
		private readonly IServiceLocator _serviceLocator;

		public WindsorMediator(IServiceLocator serviceLocator)
		{
			_serviceLocator = serviceLocator;
		}

		/// <inheritdoc />
		public async Task SendAsync<T>(T @event, Type projectionType) where T : IDomainEvent
		{
			var projector = GetProjector<T>(@event.GetType(), projectionType);

			if (projector != null)
				await projector.ProjectAsync((dynamic)@event);
		}

		private IProjector<T> GetProjector<T>(Type eventType, Type projectionType) where T : IDomainEvent
		{
			var projectors = _serviceLocator.ResolveAll(typeof(IProjector<>).MakeGenericType(eventType));

			foreach (var projector in projectors)
			{
				if (projector.GetType() == projectionType)
					return (IProjector<T>)projector;
			}

			return null;
		}
	}
}