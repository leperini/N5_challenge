using Microsoft.EntityFrameworkCore;

namespace Persistence.Options
{
    public interface IDatabaseStrategy
    {
        string GetConnectionString();
        void ConfigureOptions(DbContextOptionsBuilder optionsBuilder);
        void UseDatabase(DbContext context);
    }
}
