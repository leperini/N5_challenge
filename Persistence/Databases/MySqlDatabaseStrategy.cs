using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using Persistence.Options;

namespace Persistence.Databases
{
    public class MySqlDatabaseStrategy : DatabaseStrategyBase
    {
        public MySqlDatabaseStrategy(string connectionString) : base(connectionString)
        {
        }

        public override void ConfigureOptions(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(_connectionString);
        }

        public override void UseDatabase(DbContext context)
        {
            var mySqlConnection = context.Database.GetDbConnection() as MySqlConnection;
            mySqlConnection?.ChangeDatabase("MyOtherDatabase");
        }
    }
}
