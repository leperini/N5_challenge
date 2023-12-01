using Domain.Generics.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Databases;
using Persistence.Extensions;
using Persistence.Options;
using Persistence.Repositories;
using Persistence.Repositories.Search;


namespace Persistence
{
    public static class ServicesRegistration
    {

        public static IServiceCollection AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration, bool IsIntegrationTests)
        {

            IServiceProvider sp = services.BuildServiceProvider();

            if(IsIntegrationTests)
            {
                services.AddEntityFrameworkInMemoryDatabase();
                services.AddScoped<IDatabaseStrategy, InMemoryStrategy>(sp => new InMemoryStrategy(""));
            }
            else
            {
                var sqlServerConnectionString = configuration.GetConnectionString("SqlServer");
                //var mySqlConnectionString = configuration.GetConnectionString("MySql");

                services.AddScoped<IDatabaseStrategy, SqlServerDatabaseStrategy>(provider => new SqlServerDatabaseStrategy(sqlServerConnectionString));
                //services.AddScoped<IDatabaseStrategy, MySqlDatabaseStrategy>(provider => new MySqlDatabaseStrategy(mySqlConnectionString));

            }


            services.AddScoped<DatabaseContextFactory>();


            services.AddDbContextFactory<N5DbContext>((provider, options) =>
            {
                var databaseStrategy = services.BuildServiceProvider().GetRequiredService<IDatabaseStrategy>();
                Console.Write($"DI =>  {databaseStrategy.GetConnectionString()}");

                databaseStrategy.ConfigureOptions(options);

                //return new DatabaseContextFactory(databaseStrategy);
                // opciones para usar MySQL en lugar de SQL Server
                // options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            });

            services.AddElasticsearch(configuration);

            services.AddScoped(typeof(IRelationalDatabaseRepository<>), typeof(RelationalDatabaseRepository<>));
            services.AddScoped(typeof(ISearchRepository<>), typeof(ElasticSearchRepository<>));

            services.AddScoped<IPermissionRepository, PermissionRepository>();
            services.AddScoped<ISearchPermissionRepository, PermissionSearchRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;

        }
    }
}
