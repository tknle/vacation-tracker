using Microsoft.EntityFrameworkCore.Migrations;

namespace leave_management.Data.Migrations
{
    public partial class EmployeesUpdate3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AddColumn<string>(
            //    name: "CarriedForward",
            //    table: "AspNetUsers",
            //    nullable: true);

            //migrationBuilder.AddColumn<string>(
            //    name: "EndDate",
            //    table: "AspNetUsers",
            //    nullable: true);

            //migrationBuilder.AddColumn<string>(
            //    name: "StartDate",
            //    table: "AspNetUsers",
            //    nullable: true);

            //migrationBuilder.AddColumn<string>(
            //    name: "VacType",
            //    table: "AspNetUsers",
            //    nullable: true);

            //migrationBuilder.AddColumn<string>(
            //    name: "WorkStartDate",
            //    table: "AspNetUsers",
            //    nullable: true);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CarriedForward",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "VacType",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "WorkStartDate",
                table: "AspNetUsers");
        }
    }
}
