using System.Threading.Tasks;
using Framework.DomainModel.Core;

namespace Framework.Persistence.EventStore.Snapshot
{
    public interface ISnapshotter
    {
        Task TakeSnapshotIfNeed(IAggregateRoot aggregate);
    }
}