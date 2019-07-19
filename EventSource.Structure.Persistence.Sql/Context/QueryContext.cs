using EventSource.Structure.QueryModels.QueryModels.Company;
using Framework.QueryModel;
using System.Data.Entity;

namespace EventSource.Structure.Persistence.Sql.Context
{
	public class QueryContext : DbContext
	{
		public QueryContext() : base("ConnectionString")
		{
			Database.SetInitializer(new MigrateDatabaseToLatestVersion<QueryContext, Migrations.Configuration>());
		}

		public virtual DbSet<CompanyQueryModel> Companies { get; set; }

		public virtual DbSet<Checkpoint> Checkpoints { get; set; }
	}
}