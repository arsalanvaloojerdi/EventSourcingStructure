using EventSource.Structure.Infrastructure.Logging.Models;
using System.Data.Entity;

namespace EventSource.Structure.Infrastructure.Logging.Context
{
    public class LogContext : DbContext
    {
        public LogContext() : base("LogConnectionString")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<LogContext, Migrations.Configuration>());
        }

        public virtual DbSet<CommandLog> CommandLogs { get; set; }

        public virtual DbSet<ApiExceptionLog> ApiExceptionsLog { get; set; }
    }
}