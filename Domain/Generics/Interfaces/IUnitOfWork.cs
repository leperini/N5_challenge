
namespace Domain.Generics.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {

        IPermissionRepository _permissionRepository { get; }
        ISearchPermissionRepository _searchPermissionRepository { get; }

        void SaveChanges();
    }
}
