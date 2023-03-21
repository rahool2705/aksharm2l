namespace Business.Entities.Lead

{
    public class LeadSetting
    {
        public int LeadSettingID { get; set; }
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
