using System;
using System.ComponentModel.DataAnnotations;

namespace Business.Entities.EmployeeLoan
{
    public class EmployeeLoan
    {
        public int SrNo { get; set; }
        public int EmployeeLoanID { get; set; }

        [Required(ErrorMessage = "Please select Employee.")]
        public int? EmployeeID { get; set; }

        public string EmployeeName { get; set; }

        [Required(ErrorMessage = "Please select Payment date.")]
        public DateTime? PaymentDate { get; set; }


        [RegularExpression("^[0-9.]*$", ErrorMessage = "Loan amount be in number.")]
        public decimal? EmployeeLoanAmount { get; set; }

        [RegularExpression("^[0-9]*$", ErrorMessage = "Month tenure be in number.")]
        public int? TenureMonths { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "Interest rate be in number.")]
        public decimal? InterestRate { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "Adjustment amount be in number.")]
        public decimal? AdjustmentAmount { get; set; }
        public bool IsActive { get; set; } = true;
        public int CreatedOrModifiedBy { get; set; }
    }
}
