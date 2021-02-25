using Microsoft.EntityFrameworkCore.Migrations;

namespace leave_management.Data.Migrations
{
    public partial class Employee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AddColumn<string>(
            //    name: "CarriedForward",
            //    table: "EmployeeVM",
            //    nullable: true);

            //migrationBuilder.AddColumn<string>(
            //    name: "EndDate",
            //    table: "EmployeeVM",
            //    nullable: true);

            //migrationBuilder.AddColumn<string>(
            //    name: "StartDate",
            //    table: "EmployeeVM",
            //    nullable: true);

            //migrationBuilder.AddColumn<string>(
            //    name: "VacType",
            //    table: "EmployeeVM",
            //    nullable: true);

            //migrationBuilder.AddColumn<string>(
            //    name: "WorkStartDate",
            //    table: "EmployeeVM",
            //    nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
               name: "CarriedForward",
               table: "EmployeeVM");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "EmployeeVM");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "EmployeeVM");

            migrationBuilder.DropColumn(
                name: "VacType",
                table: "EmployeeVM");

            migrationBuilder.DropColumn(
                name: "WorkStartDate",
                table: "EmployeeVM");
        }
    }
}
