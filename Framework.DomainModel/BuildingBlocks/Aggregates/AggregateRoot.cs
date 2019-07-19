using Framework.DomainModel.BuildingBlocks.Entities;
using Framework.DomainModel.Core;
using Framework.DomainModel.Events;
using Framework.DomainModel.Extentions;
using System.Collections.Generic;
using System.Linq;

namespace Framework.DomainModel.BuildingBlocks.Aggregates
{
	public abstract class AggregateRoot<TKey> : EntityBase<TKey>, IAggregateRoot
	{
		private const int DefaultVersion = -1;
		private readonly IList<IDomainEvent> _changes;

		/// <inheritdoc />
		protected AggregateRoot(TKey id) : base(id)
		{
			this.CurrentVersion = DefaultVersion;
			this._changes = new List<IDomainEvent>();
		}

		/// <inheritdoc />
		public int CurrentVersion { get; protected set; }

		/// <inheritdoc />
		public object GetId() => this.Id;

		/// <inheritdoc />
		public IEnumerable<IDomainEvent> GetUnCommittedChanges() => this._changes.ToList();

		/// <inheritdoc />
		public void MarkChangesAsCommitted() => this._changes.Clear();

		/// <inheritdoc />
		public void LoadFromHistory(IEnumerable<IDomainEvent> events)
		{
			foreach (var @event in events)
			{
				this.ApplyChange(@event, false);
				this.CurrentVersion++;
			}
		}

		protected void ApplyChange(IDomainEvent @event)
		{
			this.ApplyChange(@event, true);
		}

		private void ApplyChange(IDomainEvent @event, bool isNew)
		{
			this.AsDynamic().Apply((dynamic)@event);
			if (isNew)
				this._changes.Add(@event);
		}

		/// <inheritdoc />
		public abstract void RestoreSnapshot(Snapshot snapshot);

		/// <inheritdoc />
		public abstract Snapshot TakeSnapshot();
	}
}