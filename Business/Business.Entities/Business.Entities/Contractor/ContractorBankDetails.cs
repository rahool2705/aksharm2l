using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Entities.Contractor
{
    public class LeadBankDetails
    {        
        public int ContractorBankDetailsID { get; set; }
        public int ContractorID { get; set; }
        public string BankName { get; set; }
        public string IFSCCode { get; set; }
        public string AccountNO { get; set; }
        public string AccountName { get; set; }
        public string BranchLocation { get; set; }
        public string City { get; set; }
        public string BankCode { get; set; }
        public string BICSwiftCode { get; set; }
        public int CountryID { get; set; }
        public string CountryName { get; set; }
        public bool IsDefaultBankAccount { get; set; }
        public int SrNo { get; set; }
        public bool IsActive { get; set; } = true;
        public int CreatedOrModifiedBy { get; set; }
    }
}
