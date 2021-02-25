using System;

using Microsoft.EntityFrameworkCore.Migrations;



namespace leave_management.Data.Migrations

{

    public partial class UpdateEmployeeAndEmployeeVM : Migration

    {

        protected override void Up(MigrationBuilder migrationBuilder)

        {

            //migrationBuilder.DropColumn(

            //    name: "PhoneNumber",

            //    table: "EmployeeVM");



            //migrationBuilder.DropColumn(

            //    name: "UserName",

            //    table: "EmployeeVM");



            migrationBuilder.AddColumn<bool>(

                name: "HalfDayOff",

                table: "LeaveRequestVM",

                nullable: false,

                defaultValue: false);



            //migrationBuilder.AddColumn<double>(

            //    name: "TotalDays",

            //    table: "LeaveRequestVM",

            //    nullable: false,

            //    defaultValue: 0.0);



            migrationBuilder.AddColumn<bool>(

                name: "HalfDayOff",

                table: "LeaveRequests",

                nullable: false,

                defaultValue: false);



            migrationBuilder.AlterColumn<string>(

                name: "VacationEntitlement",

                table: "EmployeeVM",

                nullable: true,

                oldClrType: typeof(double),

                oldType: "float");



            migrationBuilder.AlterColumn<string>(

                name: "StartDate",

                table: "EmployeeVM",

                nullable: true,

                oldClrType: typeof(DateTime),

                oldType: "datetime2");



            migrationBuilder.AlterColumn<string>(

                name: "Inactive",

                table: "EmployeeVM",

                nullable: true,

                oldClrType: typeof(bool),

                oldType: "bit",

                oldNullable: true);



            migrationBuilder.AlterColumn<string>(

                name: "DateOfBirth",

                table: "EmployeeVM",

                nullable: true,

                oldClrType: typeof(DateTime),

                oldType: "datetime2");



            //migrationBuilder.AddColumn<string>(

            //    name: "CarriedForward",

            //    table: "EmployeeVM",

            //    nullable: true);



            //migrationBuilder.AddColumn<string>(

            //    name: "EndDate",

            //    table: "EmployeeVM",

            //    nullable: true);



            //migrationBuilder.AddColumn<string>(

            //    name: "VacType",

            //    table: "EmployeeVM",

            //    nullable: true);



            migrationBuilder.AddColumn<bool>(

                name: "HalfDayOff",

                table: "LeaveRequest",

                nullable: true);



            migrationBuilder.AddColumn<bool>(

               name: "HalfDayOff",

               table: "LeaveRequestVM",

               nullable: true);



            migrationBuilder.AddColumn<bool>(

                name: "HalfDayOff",

                table: "CreateLeaveRequestVM",

                nullable: true);



            migrationBuilder.AlterColumn<string>(

                name: "VacationEntitlement",

                table: "AspNetUsers",

                nullable: true,

                oldClrType: typeof(double),

                oldType: "float",

                oldNullable: true);



            migrationBuilder.AlterColumn<string>(

                name: "StartDate",

                table: "AspNetUsers",

                nullable: true,

                oldClrType: typeof(DateTime),

                oldType: "datetime2",

                oldNullable: true);



            migrationBuilder.AlterColumn<string>(

                name: "Inactive",

                table: "AspNetUsers",

                nullable: true,

                oldClrType: typeof(bool),

                oldType: "bit",

                oldNullable: true);



            migrationBuilder.AlterColumn<string>(

                name: "DateOfBirth",

                table: "AspNetUsers",

                nullable: true,

                oldClrType: typeof(DateTime),

                oldType: "datetime2",

                oldNullable: true);



            //migrationBuilder.AddColumn<string>(

            //    name: "CarriedForward",

            //    table: "AspNetUsers",

            //    nullable: true);



            //migrationBuilder.AddColumn<string>(

            //    name: "EndDate",

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

                name: "HalfDayOff",

                table: "LeaveRequestVM");



            migrationBuilder.DropColumn(

                name: "TotalDays",

                table: "LeaveRequestVM");



            migrationBuilder.DropColumn(

                name: "HalfDayOff",

                table: "LeaveRequests");



            migrationBuilder.DropColumn(

                name: "CarriedForward",

                table: "EmployeeVM");



            migrationBuilder.DropColumn(

                name: "EndDate",

                table: "EmployeeVM");



            migrationBuilder.DropColumn(

                name: "VacType",

                table: "EmployeeVM");



            migrationBuilder.DropColumn(

                name: "WorkStartDate",

                table: "EmployeeVM");



            migrationBuilder.DropColumn(

                name: "CarriedForward",

                table: "AspNetUsers");



            migrationBuilder.DropColumn(

                name: "EndDate",

                table: "AspNetUsers");



            migrationBuilder.DropColumn(

                name: "VacType",

                table: "AspNetUsers");



            migrationBuilder.DropColumn(

                name: "WorkStartDate",

                table: "AspNetUsers");



            migrationBuilder.AlterColumn<double>(

                name: "VacationEntitlement",

                table: "EmployeeVM",

                type: "float",

               nullable: false,

                oldClrType: typeof(string),

                oldNullable: true);



            migrationBuilder.AlterColumn<DateTime>(

                name: "StartDate",

                table: "EmployeeVM",

                type: "datetime2",

                nullable: false,

                oldClrType: typeof(string),

                oldNullable: true);



            migrationBuilder.AlterColumn<bool>(

                name: "Inactive",

                table: "EmployeeVM",

                type: "bit",

                nullable: true,

                oldClrType: typeof(string),

                oldNullable: true);



            migrationBuilder.AlterColumn<DateTime>(

                name: "DateOfBirth",

                table: "EmployeeVM",

                type: "datetime2",

                nullable: false,

                oldClrType: typeof(string),

                oldNullable: true);



            migrationBuilder.AddColumn<string>(

                name: "PhoneNumber",

                table: "EmployeeVM",

                type: "nvarchar(max)",

                nullable: true);



            migrationBuilder.AddColumn<string>(

                name: "UserName",

                table: "EmployeeVM",

                type: "nvarchar(max)",

                nullable: true);



            migrationBuilder.AlterColumn<double>(

                name: "VacationEntitlement",

                table: "AspNetUsers",

                type: "float",

                nullable: true,

                oldClrType: typeof(string),

                oldNullable: true);



            migrationBuilder.AlterColumn<DateTime>(

                name: "StartDate",

                table: "AspNetUsers",

                type: "datetime2",

                nullable: true,

                oldClrType: typeof(string),

                oldNullable: true);



            migrationBuilder.AlterColumn<bool>(

                name: "Inactive",

                table: "AspNetUsers",

                type: "bit",

                nullable: true,

                oldClrType: typeof(string),

                oldNullable: true);



            migrationBuilder.AlterColumn<DateTime>(

                name: "DateOfBirth",

                table: "AspNetUsers",

                type: "datetime2",

                nullable: true,

                oldClrType: typeof(string),

                oldNullable: true);

        }

    }

}