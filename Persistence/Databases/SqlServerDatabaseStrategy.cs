using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Persistence.Options;

namespace Persistence.Databases
{
    public class SqlServerDatabaseStrategy : DatabaseStrategyBase
    {
        public SqlServerDatabaseStrategy(string connectionString) : base(connectionString)
        {
        }

        public override void ConfigureOptions(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString, sqlServerOptions => sqlServerOptions.EnableRetryOnFailure());
        }

        public override void UseDatabase(DbContext context)
        {
            var sqlConnection = context.Database.GetDbConnection() as SqlConnection;
            sqlConnection?.ChangeDatabase("MyOtherDatabase");
        }
    }
}
