using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            var permisionTypeList = new List<PermissionType>
            {
                new PermissionType
                {
                    Id = 1,
                    Description = "Vacation permissions",
                    IsDeleted = 0,
                    LastUpdated = DateTime.UtcNow,
                    CreatedAt = DateTime.UtcNow,
                },
                new PermissionType
                {
                    Id = 2,
                    Description = "Birth permissions",
                    IsDeleted = 0,
                    LastUpdated = DateTime.UtcNow,
                    CreatedAt = DateTime.UtcNow,
                }
            };

            modelBuilder.Entity<PermissionType>().HasData(permisionTypeList.ToArray());

            modelBuilder.Entity<Permission>().HasData(
                new Permission
                {
                    Id=1,
                    EmployeeForename = "Adam",
                    EmployeeSurname = "Smith",
                    PermissionTypeId = permisionTypeList[0].Id,
                    IsDeleted = 0,
                    Date = DateTime.UtcNow,
                    CreatedAt = DateTime.UtcNow,
                    LastUpdated = DateTime.UtcNow,
                    
                },
                 new Permission
                 {
                     Id=2,
                     EmployeeForename = "Barry",
                     EmployeeSurname = "Allen",
                     PermissionTypeId = permisionTypeList[1].Id,
                     IsDeleted = 0,
                     Date = DateTime.UtcNow,
                     CreatedAt = DateTime.UtcNow,
                     LastUpdated = DateTime.UtcNow,

                 }
            );
        }
    }
}
