using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Models
{
    public class HolidayVM
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Holiday")]
        public string HolidayName { get; set; }
        [Display(Name = "Date Created")]
        public DateTime? DateCreated { get; set; }
        public string Date { get; set; }
    }
}
