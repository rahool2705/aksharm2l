using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Entities.Master.EmployeeCategory
{
    public class EmployeeCategory
    {
        public int EmployeeCategoryID { get; set; }

        [Required(ErrorMessage = "Please enter the employee category name")]
        [RegularExpression("^[ A-Za-z-]*$", ErrorMessage = "Employee category name must be charater only")]        
        public string EmployeeCategoryText { get; set;}
        public int SrNo { get; set; }
        public int CreatedOrModifiedBy { get; set; }        
        public bool IsActive { get; set; } = true;

    }
}
