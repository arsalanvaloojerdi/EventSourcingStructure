using EventStore.ClientAPI;
using Framework.DomainModel.Core;
using Framework.Persistence.Core;
using Framework.Persistence.EventStore.Extentions;
using Framework.Persistence.EventStore.Snapshot;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Framework.Persistence.EventStore.UnitOfWork
{
	public class EventStoreUnitOfWork : IUnitOfWork
	{
		private readonly IList<IAggregateRoot> _trackedAggregates;
		private readonly IEventStoreConnection _context;
		private readonly ISnapshotter _snapshotter;

		public EventStoreUnitOfWork(IEventStoreConnection context, ISnapshotter snapshotter)
		{
			_context = context;
			_snapshotter = snapshotter;
			_trackedAggregates = new List<IAggregateRoot>();
		}

		/// <inheritdoc />
		public void Track(IAggregateRoot aggregateRoot)
		{
			if (AlreadyTracked(aggregateRoot))
				return;

			_trackedAggregates.Add(aggregateRoot);
		}

		/// <inheritdoc />
		public async Task CommitAsync()
		{
			await SaveAggregatesEvents();
		}

		#region InternalMethods

		private bool AlreadyTracked(IAggregateRoot aggregateRoot)
		{
			return _trackedAggregates.Any(q => q.GetType() == aggregateRoot.GetType() && q.GetId().Equals(aggregateRoot.GetId()));
		}

		private async Task SaveAggregatesEvents()
		{
			foreach (var aggregate in _trackedAggregates)
			{
				using (var transaction = await _context.StartTransactionAsync(StreamNameGenerator.Generate(aggregate), aggregate.CurrentVersion))
				{
					await transaction.WriteAsync(aggregate.GetUnCommittedChanges().ToEventData());
					await transaction.CommitAsync();
					await _snapshotter.TakeSnapshotIfNeed(aggregate);
				}
			}
		}

		#endregion
	}
}