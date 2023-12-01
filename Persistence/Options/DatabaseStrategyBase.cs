using Microsoft.EntityFrameworkCore;

namespace Persistence.Options
{
    public abstract class DatabaseStrategyBase : IDatabaseStrategy
    {
        protected readonly string _connectionString;

        public DatabaseStrategyBase(string connectionString)
        {
            _connectionString = connectionString;
        }

        public virtual string GetConnectionString()
        {
            return _connectionString;
        }

        public abstract void ConfigureOptions(DbContextOptionsBuilder optionsBuilder);

        public abstract void UseDatabase(DbContext context);
    }
}
