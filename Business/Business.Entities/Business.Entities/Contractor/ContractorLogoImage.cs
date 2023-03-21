using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Entities.Contractor
{
    public class LeadLogoImage
    {
        public int ContractorLogoImageID { get; set; }
        public int ContractorID { get; set; }
        public string LogoImageName { get; set; }
        public string LogoImagePath { get; set; }
        public bool IsActive { get; set; } = true;
        public int? CreatedOrModifiedBy { get; set; }
    }
}
