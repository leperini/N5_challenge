using Domain.Generics;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Permission : BaseEntity
    {
        public string EmployeeForename { get; set; }

        public string EmployeeSurname { get; set; }

        public DateTime Date { get; set; }

        public int PermissionTypeId { get; set; }

        [ForeignKey(name: "PermissionTypeId")]
        public PermissionType PermissionType { get; set; }



    }
}