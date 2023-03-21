namespace Business.Entities.Customer
{
    public class CustomerSetting
    {
        public int CustomerSettingID { get; set; }
        public int CustomerID { get; set; }
        public string CreditLimit { get; set; }
        public string CommitementLimit { get; set; }
        public string PaymentTerm { get; set; }
        public string AllowDiscountPerPO { get; set; }
    }
}
