using System;
using System.Threading.Tasks;

namespace Framework.Persistence.EventStore.Snapshot
{
    using DomainModel.Core;

    public interface ISnapshotReader
    {
        Task<Snapshot> ReadSnapshot(object id, Type aggregateType);
    }
}