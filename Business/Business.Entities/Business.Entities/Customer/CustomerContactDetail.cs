namespace Business.Entities.Customer
{
    public class CustomerContactDetail
    {
        public int CustomerContactDetailID { get; set; }
        public int? CustomerID { get; set; }
        public string OfficePhone { get; set; }
        public string MobileNo { get; set; }
        public string Website { get; set; }
    }
}
