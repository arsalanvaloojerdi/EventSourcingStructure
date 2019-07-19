using EventSource.Structure.Persistence.Sql.Context;
using Framework.QueryModel;
using System.Data.Entity;
using System.Threading.Tasks;

namespace EventSource.Structure.Persistence.Sql.Repositories
{
	public class CheckpointStore : ICheckpointStore
	{
		private readonly QueryContext _context;
		private readonly DbSet<Checkpoint> _dbset;

		/// <inheritdoc />
		public CheckpointStore()
		{
			_context = new QueryContext();
			_dbset = _context.Set<Checkpoint>();
		}

		/// <inheritdoc />
		public async Task Set(Checkpoint checkpoint)
		{
			var lastCheckpoint = await _dbset.FirstOrDefaultAsync(q => q.ProjectionName.Equals(checkpoint.ProjectionName));
			if (lastCheckpoint != null)
			{
				lastCheckpoint.Position = checkpoint.Position;
			}
			else
			{
				_dbset.Add(checkpoint);
			}

			await _context.SaveChangesAsync();
		}

		public Task<Checkpoint> Get(string projectionName)
		{
			return _dbset.FirstOrDefaultAsync(q => q.ProjectionName == projectionName);
		}
	}
}