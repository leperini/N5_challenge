using Microsoft.EntityFrameworkCore;
using Persistence.Options;

namespace Persistence.Databases
{
    public class InMemoryStrategy : DatabaseStrategyBase
    {
        private readonly DbContextOptions<DbContext> _options;

        public InMemoryStrategy(string connectionString) : base(connectionString)
        {
        }

        public override void ConfigureOptions(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("InMemoryN5Db");
        }

        public override void UseDatabase(DbContext context)
        {
            throw new NotImplementedException();
        }
    }
}
