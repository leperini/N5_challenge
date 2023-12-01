using Domain.Generics;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class PermissionType : BaseEntity
    {
        public string Description { get; set; }

        [InverseProperty(property: "PermissionType")]
        public List<Permission> Permissions { get; set; }

        public PermissionType()
        {
            Permissions = new List<Permission>();
        }
    }
}
