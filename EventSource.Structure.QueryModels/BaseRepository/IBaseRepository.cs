using Framework.QueryModel;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EventSource.Structure.QueryModels.BaseRepository
{
	public interface IBaseRepository<T> where T : IQueryModel
	{
		Task<bool> IsExistsAsync(Guid id);

		Task AddAsync(T entity);

		IQueryable<T> AsQuery();

		Task SaveChangesAsync();
	}
}