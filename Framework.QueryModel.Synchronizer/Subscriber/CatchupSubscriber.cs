using Castle.MicroKernel.Lifestyle;
using Castle.Windsor;
using EventStore.ClientAPI;
using Framework.Core;
using Framework.Persistence.EventStore;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Framework.QueryModel.Synchronizer.Subscriber
{
	public class CatchupSubscriber<T> : ISubscriber where T : IProjection
	{
		private readonly IEventStoreConnection _connection;
		private readonly ICheckpointStore _checkpointStore;
		private readonly IMediator _mediator;
		private readonly IServiceLocator _serviceLocator;

		protected CatchupSubscriber(
			IEventStoreConnection connection,
			ICheckpointStore checkpointStore,
			IMediator mediator,
			IServiceLocator serviceLocator)
		{
			_connection = connection;
			_checkpointStore = checkpointStore;
			_mediator = mediator;
			_serviceLocator = serviceLocator;
		}

		/// <inheritdoc />
		public virtual async Task SubscribeAsync()
		{
			await _connection.ConnectAsync();

			_connection.SubscribeToAllFrom(await GetCheckpointPosition(), CatchUpSubscriptionSettings.Default,
				EventAppeared(), LiveProcessingStarted(), SubscriptionDropped(), DefaultUserCredentials.Value);
		}

		protected Action<EventStoreCatchUpSubscription, ResolvedEvent> EventAppeared()
		{
			return (subscription, @event) =>
			{
				if (IsSystemEvents(@event))
					return;

				if (EventIsFromAnotherContext(@event))
					return;

				if (IsSnapshot(@event))
					return;

				var deserializedEvent = JsonConvert.DeserializeObject(
					Encoding.UTF8.GetString(@event.Event.Data),
					TypeResolver.ResolveDomainEventType(@event.Event.EventType));

				using (var container = _serviceLocator.GetInstance<IWindsorContainer>().BeginScope())
				{
					_mediator.SendAsync((dynamic)deserializedEvent, typeof(T)).Wait();
				}

				_checkpointStore.Set(new Checkpoint(typeof(T).Name, @event.OriginalPosition.ToString())).Wait();
			};
		}

		protected virtual async Task<Position> GetCheckpointPosition()
		{
			var checkpoint = await _checkpointStore.Get(typeof(T).Name);

			if (checkpoint != null)
			{
				var checkpointPosition = checkpoint.Position.Split('/');

				return new Position(Convert.ToInt64(checkpointPosition[0]), Convert.ToInt64(checkpointPosition[1]));
			}
			else
			{
				return Position.Start;
			}
		}

		protected virtual Action<EventStoreCatchUpSubscription, SubscriptionDropReason, Exception> SubscriptionDropped()
		{
			return (subscription, droppedReason, exception) => { };
		}

		protected virtual Action<EventStoreCatchUpSubscription> LiveProcessingStarted()
		{
			return subscription => { };
		}

		#region InternalMethods

		private static bool IsSystemEvents(ResolvedEvent @event)
		{
			return @event.Event.EventType.StartsWith("$");
		}

		private static bool IsSnapshot(ResolvedEvent @event)
		{
			return @event.Event.EventType.Contains("Snapshot");
		}

		private static bool EventIsFromAnotherContext(ResolvedEvent @event)
		{
			return !@event.Event.EventStreamId.StartsWith("EventSourceStructure");
		}

		#endregion
	}
}