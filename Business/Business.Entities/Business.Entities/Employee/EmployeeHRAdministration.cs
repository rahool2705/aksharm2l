using System;

namespace Business.Entities.Employee
{
    public class EmployeeHRAdministration
    {
        public int EmployeeAdminID { get; set; }
        public int EmployeeID { get; set; }
        public int CompanyID { get; set; }
        public int EmployeeCategoryID { get; set; }
        public int EmployeeCategorytext { get; set; }
        public DateTime? JoiningDate { get; set; }
        public int EmploymentStatusID { get; set; }
        public DateTime? ConfirmationDate { get; set; }
        public int EmploymentTypeID { get; set; }
        public string EmploymentTypeText { get; set; }
        public string YearlyCTC { get; set; }
        public string MonthlyCTC { get; set; }
        public int CreatedOrModifiedBy { get; set; }
        public int ContractorID { get; set; }
        public string ContractorName { get; set; }
    }
}
