using Domain.Entities;
using Domain.Generics.Interfaces;

namespace Persistence.Repositories
{
    public class PermissionRepository : RelationalDatabaseRepository<Permission>, IPermissionRepository
    {

        private readonly N5DbContext _dbContext;

        public PermissionRepository(N5DbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }



    }
}
