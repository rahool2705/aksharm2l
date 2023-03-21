using System.Data;

namespace Business.Entities.Customer
{
    public class CustomerRegistration
    {
        public int CustomerRegistrationID { get; set; }
        public int CustomerID { get; set; }
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
        public int CreatedOrModifiedBy { get; set; }
    }
}
