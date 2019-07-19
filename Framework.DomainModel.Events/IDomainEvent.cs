using System;

namespace Framework.DomainModel.Events
{
    public interface IDomainEvent
    {
        Guid EventId { get; }

        Guid AggregateId { get; }

        Guid CorrelationId { get; }

        DateTime OccuredDate { get; }
    }
}