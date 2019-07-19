using EventStore.ClientAPI;
using Framework.Persistence.EventStore.Extentions;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Persistence.EventStore.Snapshot
{
	using DomainModel.Core;

	public class SnapshotReader : ISnapshotReader
	{
		private readonly IEventStoreConnection _connection;

		public SnapshotReader(IEventStoreConnection connection)
		{
			_connection = connection;
		}

		/// <inheritdoc />
		public async Task<Snapshot> ReadSnapshot(object id, Type aggregateType)
		{
			var snapshot = await _connection.ReadEventAsync(StreamNameResolver.ResolveForSnapshot(id, aggregateType), StreamPosition.End, true);

			if (NotAnySnapshotExists(snapshot))
				return await Task.FromResult<Snapshot>(null);

			return (Snapshot)JsonConvert.DeserializeObject(
				// ReSharper disable once PossibleInvalidOperationException
				Encoding.UTF8.GetString(snapshot.Event.Value.Event.Data), TypeResolver.ResolveSnapshotType(snapshot.Event.Value.Event.EventType));
		}

		#region InternalMethods

		private static bool NotAnySnapshotExists(EventReadResult snapshot)
		{
			return !snapshot.Event.HasValue;
		}

		#endregion
	}
}