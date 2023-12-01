using Domain.Generics.Interfaces;

namespace Persistence.Repositories
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly N5DbContext dbContext;

        public IPermissionRepository _permissionRepository { get; }
        public ISearchPermissionRepository _searchPermissionRepository { get; }

        public UnitOfWork(N5DbContext dbContext, IPermissionRepository permissionRepository, ISearchPermissionRepository searchPermissionRepository)
        {
            this.dbContext = dbContext;

            _permissionRepository = permissionRepository;
            _searchPermissionRepository = searchPermissionRepository;
        }

        public void Dispose() => dbContext.Dispose();

        public void SaveChanges() => dbContext.SaveChanges();

    }
}
