using System;

namespace Framework.DomainModel.Core
{
	public abstract class Snapshot
	{
		protected Snapshot() { }

		/// <inheritdoc />
		protected Snapshot(Guid aggregateId, int version)
		{
			this.AggregateId = aggregateId;
			this.Version = version;
		}

		public Guid AggregateId { get; set; }

		public int Version { get; set; }
	}
}