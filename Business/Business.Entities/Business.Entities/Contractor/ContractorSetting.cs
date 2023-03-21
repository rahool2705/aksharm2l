using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Entities.Contractor
{
    public class LeadSetting
    {
        public int ContractorSettingID { get; set; }
        public int ContractorID { get; set; }
        public string CreditLimit { get; set; }
        public string CommitementLimit { get; set; }
        public string PaymentTerm { get; set; }
        public string AllowDiscountPerPO { get; set; }
        public int SrNo { get; set; }
        public bool IsActive { get; set; } = true;
        public int CreatedOrModifiedBy { get; set; }
    }
}
