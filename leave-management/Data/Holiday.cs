using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Data
{
    public class Holiday
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string HolidayName { get; set; }
        public DateTime DateCreated { get; set; }
        public string Date { get; set; }
    }
}
