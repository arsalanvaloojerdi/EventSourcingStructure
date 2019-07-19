using System;

namespace Framework.DomainModel.Events
{
	public class DomainEvent : IDomainEvent
	{
		/// <inheritdoc />
		public DomainEvent(Guid aggregateId)
		{
			this.EventId = Guid.NewGuid();
			this.AggregateId = aggregateId;
			this.CorrelationId = Guid.NewGuid();
			this.OccuredDate = DateTime.Now;
		}

		/// <inheritdoc />
		public DomainEvent(Guid aggregateId, Guid correlationId)
		{
			this.AggregateId = aggregateId;
			this.CorrelationId = correlationId;
		}

		/// <inheritdoc />
		public Guid EventId { get; }

		/// <inheritdoc />
		public Guid AggregateId { get; }

		/// <inheritdoc />
		public Guid CorrelationId { get; }

		/// <inheritdoc />
		public DateTime OccuredDate { get; }

		protected DomainEvent() { }
	}
}