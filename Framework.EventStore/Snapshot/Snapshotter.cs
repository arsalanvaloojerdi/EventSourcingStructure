using EventStore.ClientAPI;
using Framework.DomainModel.Core;
using Framework.Persistence.EventStore.Extentions;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Persistence.EventStore.Snapshot
{
	public class Snapshotter : ISnapshotter
	{
		private readonly IEventStoreConnection _connection;

		public Snapshotter(IEventStoreConnection connection)
		{
			_connection = connection;
		}

		/// <inheritdoc />
		public async Task TakeSnapshotIfNeed(IAggregateRoot aggregate)
		{
			var lastSnapshotVersion = -1L;
			var lastSnapshot = await _connection.ReadEventAsync(StreamNameResolver.ResolveForSnapshot(aggregate), StreamPosition.End, true);

			if (AnySnapshotExists(lastSnapshot))
				lastSnapshotVersion = lastSnapshot.Event.GetValueOrDefault().OriginalEventNumber;

			if (NeedToTakeSnapshot(aggregate, lastSnapshotVersion))
				await TakeSnapshot(_connection, aggregate);
		}

		#region InternalMethods

		private static async Task TakeSnapshot(IEventStoreConnection connection, IAggregateRoot aggregate)
		{
			var snapshot = aggregate.TakeSnapshot();
			var encodedEvent = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(snapshot));
			var eventData = new EventData(Guid.NewGuid(), snapshot.GetType().Name, true, encodedEvent, null);

			await connection.AppendToStreamAsync(StreamNameGenerator.GenerateForSnapshot(aggregate), ExpectedVersion.Any, eventData);
		}

		private static bool AnySnapshotExists(EventReadResult lastSnapshot)
		{
			return lastSnapshot.Event.HasValue;
		}

		private static bool NeedToTakeSnapshot(IAggregateRoot aggregate, long lastSnapshotVersion)
		{
			var neededEventsToSnapshot = 1;

			return aggregate.CurrentVersion - lastSnapshotVersion >= neededEventsToSnapshot;
		}

		#endregion
	}
}