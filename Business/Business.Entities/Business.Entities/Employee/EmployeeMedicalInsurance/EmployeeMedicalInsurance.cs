using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Entities.Employee.EmployeeMedicalInsurance
{
    public class EmployeeMedicalInsurance
    {
        public int EmployeeMedicalInsuranceID { get; set; }
        public int EmployeeID { get; set; }
        public string InsuranceCompany { get; set; }
        public string PolicyName { get; set; }
        public string PolicyNumber { get; set; }
        public DateTime ? PolicyStartDate { get; set; }
        public DateTime? PolicyExpiryDate { get; set; }
        public decimal? PolicyPremiumAmt { get; set; }
        public string AgentName { get; set; }
        public int SrNo { get; set; }
        public int CreatedOrModifiedBy { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
