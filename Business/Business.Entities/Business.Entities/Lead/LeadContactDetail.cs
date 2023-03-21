namespace Business.Entities.Lead
{
    public class LeadContactDetail
    {
        public int LeadContactDetailID { get; set; }
        public int? LeadID { get; set; }
        public string OfficePhone { get; set; }
        public string MobileNo { get; set; }
        public string Website { get; set; }
        public int SrNo { get; set; }
        public bool IsActive { get; set; } = true;
        public int CreatedOrModifiedBy { get; set; }
    }
}
