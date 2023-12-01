using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Extensions;
using Persistence.Options;

namespace Persistence
{
    public class N5DbContext : DbContext
    {
        private readonly IDatabaseStrategy _databaseStrategy;


        public N5DbContext(DbContextOptions<N5DbContext> options, IDatabaseStrategy databaseStrategy)
          : base(options)
        {
            _databaseStrategy = databaseStrategy;
        }
        public N5DbContext(IDatabaseStrategy databaseStrategy)
        {
            _databaseStrategy = databaseStrategy;
        }

        public N5DbContext() : base()
        {
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            _databaseStrategy.ConfigureOptions(optionsBuilder);
        }

        public void UseOtherDatabase()
        {
            _databaseStrategy.UseDatabase(this);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Seed();

            base.OnModelCreating(modelBuilder);

        }

        public DbSet<PermissionType> PermissionTypes { get; set; }
        public DbSet<Permission> Permissions { get; set; }
    }
}
