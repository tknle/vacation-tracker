using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Models
{
    public class EmployeeVM
    {
        /*add more from the ASP net user id, check the type of data within the asp table. i.e, 
         * go to SQL Server Object Explorer > SQL Server > (localdb)\MSSQLLocal... > Databases >
         * leave_management > Tables > dbo.AspNetUsers > Columns to check the type of each column.
         * Mismatch type will result in errors.
         */
        public string Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string TaxId { get; set; }
        public string DateOfBirth { get; set; }
        public string WorkStartDate { get; set; }
        public string EmployeeClass { get; set; }
        public string Inactive { get; set; }
        public string Department { get; set; }
        public string Manager { get; set; }
        public string Office { get; set; }
        public string TypeOfEmp { get; set; }
        public string HRStatus { get; set; }
        public string Title { get; set; }
        public string VacationEntitlement { get; set; }
        public string CarriedForward { get; set; }
        public string VacType { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }

    }
}
