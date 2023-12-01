using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Persistence.Options;

namespace Persistence.Databases
{
    public class DatabaseContextFactory : IDesignTimeDbContextFactory<N5DbContext>
    {

        private readonly IDatabaseStrategy _databaseStrategy;
        // public IDatabaseStrategy _databaseStrategy { get; set; }


        //public DatabaseContextFactory()
        //{
        //    _databaseStrategy = new SqlServerDatabaseStrategy("Server=127.0.0.1,11433;Database=challenge_n5;User ID=sa;Password=SqlPass2023*;TrustServerCertificate=True;");
        //}
        /*public DatabaseContextFactory() : this(null)
        {
        }*/


        public DatabaseContextFactory(IDatabaseStrategy databaseStrategy)
        {
            _databaseStrategy = databaseStrategy;
            //Console.WriteLine("Creo contexto");
        }

        public N5DbContext CreateDbContext(string[] args)
        {
            try
            {
                Console.WriteLine($"{args.Length}");
                var optionsBuilder = new DbContextOptionsBuilder<N5DbContext>();
                _databaseStrategy.ConfigureOptions(optionsBuilder);
                return new N5DbContext(optionsBuilder.Options, _databaseStrategy);
            }
            catch (Exception e)
            {
                //Console.WriteLine(e.Message);
                throw e?.InnerException;
            }

        }
    }
}
