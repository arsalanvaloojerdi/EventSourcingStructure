using EventSource.Structure.Persistence.Sql.Context;
using EventSource.Structure.QueryModels.BaseRepository;
using Framework.QueryModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace EventSource.Structure.Persistence.Sql.Repositories
{
	public class BaseRepository<T> : IBaseRepository<T> where T : class, IQueryModel
	{
		private readonly QueryContext _context;
		private readonly DbSet<T> _dbset;

		public BaseRepository(QueryContext context)
		{
			_context = context;
			_dbset = context.Set<T>();
		}

		/// <inheritdoc />
		public async Task<bool> IsExistsAsync(Guid id)
		{
			return await _dbset.FindAsync(id) != null;
		}

		/// <inheritdoc />
		public Task<List<T>> GetAllAsync()
		{
			return _dbset.ToListAsync();
		}

		/// <inheritdoc />
		public Task<T> GetByIdAsync(object id)
		{
			return _dbset.FindAsync(id);
		}

		/// <inheritdoc />
		public IQueryable<T> AsQuery()
		{
			return _dbset.AsQueryable();
		}

		public IBaseRepository<T> Include(string include)
		{
			_dbset.Include(include);

			return this;
		}

		/// <inheritdoc />
		public async Task AddAsync(T entity)
		{
			_dbset.Add(entity);

			await _context.SaveChangesAsync();
		}

		/// <inheritdoc />
		public async Task DeleteAsync(object id)
		{
			var entity = await _dbset.FindAsync(id);
			if (entity == null)
				throw new Exception();

			_dbset.Remove(entity);
			await _context.SaveChangesAsync();
		}

		/// <inheritdoc />
		public async Task SaveChangesAsync()
		{
			await _context.SaveChangesAsync();
		}
	}
}