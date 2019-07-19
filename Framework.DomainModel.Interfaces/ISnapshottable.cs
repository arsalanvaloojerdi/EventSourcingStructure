namespace Framework.DomainModel.Core
{
    public interface ISnapshottable
    {
        void RestoreSnapshot(Snapshot snapshot);

        Snapshot TakeSnapshot();
    }
}