using System;

namespace Business.Entities.Customer
{
    public class CustomerBankDetails
    {
        public int SrNo { get; set; }
        public int CustomerBankDetailsID { get; set; }
        public int CustomerID { get; set; }
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
        public bool IsActive { get; set; } = true;
        public int CreatedOrModifiedBy { get; set; }
    }
}
