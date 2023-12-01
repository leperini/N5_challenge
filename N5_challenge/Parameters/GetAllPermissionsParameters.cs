using Domain.Models;

namespace N5_challenge.Parameters
{
    public class GetAllPermissionsParameters  : PaginatorModel
    {
        public int PermissionTypeId { get; set; }

    }
}
