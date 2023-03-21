namespace Business.Entities.Lead
{
    public class LeadLogoImage
    {
        public int LeadLogoImageID { get; set; }
        public int LeadID { get; set; }
        public string LogoImageName { get; set; }
        public string LogoImagePath { get; set; }
        public bool IsActive { get; set; } = true;
        public int? CreatedOrModifiedBy { get; set; }
    }
}
