using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Presentation.Models
{
    public class EmployeeViewModel
    {
        [Required]
        [Display(Name = "Employee Number")]
        public int EmployeeNumber { get; set; }
        [Required]
        [RegularExpression(@"^([a-zA-Z0-9 \.\-]+)$", ErrorMessage = "Invalid Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Date Of Joining")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime DateOfJoining { get; set; }
        [Required]
        public string Designation { get; set; }
        [Required]
        public string Band { get; set; }


    }
}
