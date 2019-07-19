using System.Threading.Tasks;
using Framework.DomainModel.Core;

namespace Framework.Persistence.EventStore.Repository
{
    public interface IBaseRepository<TEntity> where TEntity : IAggregateRoot
    {
        Task<TEntity> GetAsync(object id);

        void Add(TEntity entity);
    }
}