using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Entities.Contractor
{
    public class LeadContactDetail
    {
        public int ContractorContactDetailID { get; set; }
        public int? ContractorID { get; set; }
        public string OfficePhone { get; set; }
        public string MobileNo { get; set; }
        public string Website { get; set; }
        public int SrNo { get; set; }
        public bool IsActive { get; set; } = true;
        public int CreatedOrModifiedBy { get; set; }
    }
}
