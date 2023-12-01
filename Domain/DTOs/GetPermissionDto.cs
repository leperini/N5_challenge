namespace Domain.DTOs
{
    public class GetPermissionDto
    {
        public int PermissionId { get; set; }

        public string? EmployeeForename { get; set; }

        public string? EmployeeSurname { get; set; }

        public DateTime PermissionDate { get; set; }

        public string? PermissionType { get; set; }

    }
}
