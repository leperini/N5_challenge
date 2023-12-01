using Domain.Entities;
using Domain.Generics.Interfaces;

namespace Domain.Generics.Interfaces
{
    public interface IPermissionRepository : IRelationalDatabaseRepository<Permission>
    {

    }
}
