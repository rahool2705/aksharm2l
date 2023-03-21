using Microsoft.AspNetCore.Http;

namespace Business.Entities.Lead
{
    public class LeadMaster
    {
        public int LeadID { get; set; }
        public string UnitNoName { get; set; }
        public string LeadCode { get; set; }
        public string LeadName { get; set; }
        public string GroupName { get; set; }
        public string OwnerName { get; set; }
        public string ContactPhone1 { get; set; }
        public string Mobile1 { get; set; }
        public string FaxNo { get; set; }
        public int? IndustryTypeID { get; set; }
        public int? BusinessTypeID { get; set; }
        public string LogoImagePath { get; set; }        
        public IFormFile LogoImage { get; set; }
        public string LogoImageName { get; set; }
        public bool IsActive { get; set; } = true;
        public int? CreatedOrModifiedBy { get; set; }
        public int SrNo { get; set; }
    }
}
