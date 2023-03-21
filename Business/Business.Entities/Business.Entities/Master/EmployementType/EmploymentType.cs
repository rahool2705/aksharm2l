using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Entities.Master.EmployementType
{
    public class EmploymentType
    {
        public int EmploymentTypeID { get; set; }
        [Required(ErrorMessage = "Please enter the employment type name")]
        [RegularExpression("^[ A-Za-z-]*$", ErrorMessage = "Employment type name must be charater only")]
        public string EmploymentTypeText { get; set; }
        public object SrNo { get; set; }
        public int CreatedOrModifiedBy { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
