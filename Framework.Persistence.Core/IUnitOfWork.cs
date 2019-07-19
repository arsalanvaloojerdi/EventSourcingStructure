using System.Threading.Tasks;
using Framework.DomainModel.Core;

namespace Framework.Persistence.Core
{
    public interface IUnitOfWork
    {
        void Track(IAggregateRoot aggregateRoot);

        Task CommitAsync();
    }
}