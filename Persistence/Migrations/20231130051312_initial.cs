using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PermissionTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeForename = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeSurname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PermissionTypeId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Permissions_PermissionTypes_PermissionTypeId",
                        column: x => x.PermissionTypeId,
                        principalTable: "PermissionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "PermissionTypes",
                columns: new[] { "Id", "CreatedAt", "Description", "IsDeleted", "LastUpdated" },
                values: new object[] { 1, new DateTime(2023, 11, 30, 5, 13, 11, 815, DateTimeKind.Utc).AddTicks(2075), "Vacation permissions", 0, new DateTime(2023, 11, 30, 5, 13, 11, 815, DateTimeKind.Utc).AddTicks(2071) });

            migrationBuilder.InsertData(
                table: "PermissionTypes",
                columns: new[] { "Id", "CreatedAt", "Description", "IsDeleted", "LastUpdated" },
                values: new object[] { 2, new DateTime(2023, 11, 30, 5, 13, 11, 815, DateTimeKind.Utc).AddTicks(2080), "Birth permissions", 0, new DateTime(2023, 11, 30, 5, 13, 11, 815, DateTimeKind.Utc).AddTicks(2080) });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "CreatedAt", "Date", "EmployeeForename", "EmployeeSurname", "IsDeleted", "LastUpdated", "PermissionTypeId" },
                values: new object[] { 1, new DateTime(2023, 11, 30, 5, 13, 11, 815, DateTimeKind.Utc).AddTicks(2243), new DateTime(2023, 11, 30, 5, 13, 11, 815, DateTimeKind.Utc).AddTicks(2243), "Adam", "Smith", 0, new DateTime(2023, 11, 30, 5, 13, 11, 815, DateTimeKind.Utc).AddTicks(2243), 1 });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "CreatedAt", "Date", "EmployeeForename", "EmployeeSurname", "IsDeleted", "LastUpdated", "PermissionTypeId" },
                values: new object[] { 2, new DateTime(2023, 11, 30, 5, 13, 11, 815, DateTimeKind.Utc).AddTicks(2297), new DateTime(2023, 11, 30, 5, 13, 11, 815, DateTimeKind.Utc).AddTicks(2296), "Barry", "Allen", 0, new DateTime(2023, 11, 30, 5, 13, 11, 815, DateTimeKind.Utc).AddTicks(2297), 2 });

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_PermissionTypeId",
                table: "Permissions",
                column: "PermissionTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "PermissionTypes");
        }
    }
}
