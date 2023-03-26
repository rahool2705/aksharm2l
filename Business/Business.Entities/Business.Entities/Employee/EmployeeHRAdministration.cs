using System;
using System.ComponentModel.DataAnnotations;

namespace Business.Entities.Employee
{
    public class EmployeeHRAdministration
    {
        public int EmployeeAdminID { get; set; }
        public int EmployeeID { get; set; }
        public int? CompanyID { get; set; }
        public int? EmployeeCategoryID { get; set; }
        public int EmployeeCategorytext { get; set; }
        public DateTime? JoiningDate { get; set; }
        public int? EmploymentStatusID { get; set; }
        public DateTime? ConfirmationDate { get; set; }
        public int? EmploymentTypeID { get; set; }
        public string EmploymentTypeText { get; set; }

        [Required(ErrorMessage = "Yearly CTC is cannot be null or empty.")]
        [RegularExpression("^[0-9.]*$", ErrorMessage = "Yearly CTC be in number.")]
        public decimal? YearlyCTC { get; set; }

        [Required(ErrorMessage = "Monthly CTC is cannot be null or empty.")]
        [RegularExpression("^[0-9.]*$", ErrorMessage = "Monthly CTC be in number.")]
        public decimal? MonthlyCTC { get; set; }
        public int CreatedOrModifiedBy { get; set; }
        public int? ContractorID { get; set; }
        public string ContractorName { get; set; }
    }
}
