using System.Collections.Generic;
using Framework.DomainModel.Events;

namespace Framework.DomainModel.Core
{
    public interface IAggregateRoot : ISnapshottable
    {
        int CurrentVersion { get; }

        object GetId();

        IEnumerable<IDomainEvent> GetUnCommittedChanges();

        void MarkChangesAsCommitted();

        void LoadFromHistory(IEnumerable<IDomainEvent> events);
    }
}