using EventStore.ClientAPI;
using Framework.DomainModel.Core;
using Framework.Persistence.Core;
using Framework.Persistence.EventStore.Extentions;
using Framework.Persistence.EventStore.Snapshot;
using System.Threading.Tasks;

namespace Framework.Persistence.EventStore.Repository
{
	public class EventStoreBaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : IAggregateRoot
	{
		private readonly IEventStoreConnection _connection;
		private readonly IUnitOfWork _unitOfWork;
		private readonly ISnapshotReader _snapshotReader;

		public EventStoreBaseRepository(IEventStoreConnection connection, IUnitOfWork unitOfWork, ISnapshotReader snapshotReader)
		{
			_connection = connection;
			_unitOfWork = unitOfWork;
			_snapshotReader = snapshotReader;
		}

		/// <inheritdoc />
		public async Task<TEntity> GetAsync(object id)
		{
			var aggregateRoot = AggregateFactory.Build(typeof(TEntity));

			var snapshot = await _snapshotReader.ReadSnapshot(id, typeof(TEntity));
			if (snapshot != null)
				aggregateRoot.RestoreSnapshot(snapshot);

			var streamName = StreamNameResolver.Resolve(id, typeof(TEntity));

			var slice = GetEventSlice(_connection, streamName, aggregateRoot.CurrentVersion == -1 ? StreamPosition.Start : aggregateRoot.CurrentVersion + 1);

			if (slice.Status == SliceReadStatus.StreamDeleted || slice.Status == SliceReadStatus.StreamNotFound)
			{
				return await Task.FromResult(default(TEntity));
			}

			aggregateRoot.LoadFromHistory(slice.Events.ToDomainEvent());

			while (!slice.IsEndOfStream)
			{
				slice = GetEventSlice(_connection, streamName, slice.NextEventNumber);
				aggregateRoot.LoadFromHistory(slice.Events.ToDomainEvent());
			}

			_unitOfWork.Track(aggregateRoot);

			return (TEntity)aggregateRoot;
		}

		/// <inheritdoc />
		public void Add(TEntity entity)
		{
			_unitOfWork.Track(entity);
		}

		#region InternalMethods

		private static StreamEventsSlice GetEventSlice(IEventStoreConnection context, string streamName, long startPosition)
		{
			return context.
				ReadStreamEventsForwardAsync(streamName, startPosition, 4096, true).Result;
		}

		#endregion
	}
}