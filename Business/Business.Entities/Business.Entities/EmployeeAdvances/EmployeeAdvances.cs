using System;
using System.ComponentModel.DataAnnotations;

namespace Business.Entities.EmployeeAdvances
{
    public class EmployeeAdvances
    {
        public int SrNo { get; set; }
        public int EmployeeAdvancesID { get; set; }

        [Required(ErrorMessage = "Please select Employee Name.")]
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }

        [Required(ErrorMessage = "Please select Payment date.")]
        public DateTime? PaymentDate { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "Amount be in number.")]
        public int? Amount { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; } = true;
        public int CreatedOrModifiedBy { get; set; }
    }
}
