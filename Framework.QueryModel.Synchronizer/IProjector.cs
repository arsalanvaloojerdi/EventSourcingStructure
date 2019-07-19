using Framework.DomainModel.Events;
using System.Threading.Tasks;

namespace Framework.QueryModel.Synchronizer
{
    public interface IProjector { }

    public interface IProjector<T> : IProjector where T : IDomainEvent
    {
        Task ProjectAsync(T @event);
    }
}