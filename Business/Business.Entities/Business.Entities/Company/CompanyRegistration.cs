using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Entities.Company
{
    public class CompanyRegistration
    {
        public int CompanyRegistrationID { get; set; }
        public int CompanyID { get; set; }
        public string PANNo { get; set; }
        public string GSTINNo { get; set; }
        public string GSTINType { get; set; }
        public string FactoryLicenseNo { get; set; }
        public string FactoryRegNo { get; set; }
        public string ARNNo { get; set; }
        public string ECCNo { get; set; }
        public string MSMENo { get; set; }
        public string SSINo { get; set; }
        public string TANNo { get; set; }
        public string ExportNo { get; set; }
        public string ImportNo { get; set; }
        public string TaxRange { get; set; }
        public string TaxDivisio { get; set; }
        public string TaxCommisionerate { get; set; }
        public string LabourContratorNo { get; set; }
        public int SrNo { get; set; }
        public bool IsActive { get; set; } = true;
        public int CreatedOrModifiedBy { get; set; }
    }
}
