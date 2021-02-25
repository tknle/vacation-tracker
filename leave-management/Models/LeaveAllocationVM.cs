using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Models
{
    public class LeaveAllocationVM
    {
        public int Id { get; set; }
        public int NumberOfDays { get; set; }
        public DateTime DateCreated { get; set; }
        //Create List of Employee and corresponding Id
        public int Period { get; set; }
        public EmployeeVM Employee { get; set; }
        public string EmployeeId { get; set; }
        //list of Leave Type and correspoding Id for leave type
        public LeaveTypeVM LeaveType { get; set; }
        public int LeaveTypeId { get; set; }
        
    }
    public class CreateLeaveAllocationVM
    {
        public int NumberUpdated { get; set; }
        public List<LeaveTypeVM> LeaveTypes { get; set; }
    }

    public class EditLeaveAllocationVM
    {
        public int id { get; set; }
        public EmployeeVM Employee { get; set; }
        public string EmployeeId { get; set; }
        public int NumberOfDays { get; set; }
        public LeaveTypeVM LeaveType { get; set; }
    }

    public class ViewAllocationsVM
    {
        public EmployeeVM Employee { get; set; }
        public string EmployeeId { get; set; }
        public List<LeaveAllocationVM> LeaveAllocations { get; set; }
    }
}
