using System;

namespace Business.Entities.Employee
{
    public class EmployeeExperience
    {
        public int SrNo { get; set; }
        public int EmployeeExperienceID { get; set; }
        public int EmployeeID { get; set; }
        public string JobTitle { get; set; }
        public int EmploymentTypeID { get; set; }
        public string CompanyName { get; set; }
        public string Location { get; set; }
        public int LocationTypeID { get; set; }
        public bool IsCurrentlyWorking { get; set; } = true;
        public string StartMonth { get; set; }
        public string EndMonth { get; set; }
        public int StartYear { get; set; }
        public int EndYear { get; set; }
        public int IndustryTypeID { get; set; }
        public string CompanyDescription { get; set; }
        public string ProfileHeadLine { get; set; }
        public string Skills { get; set; }
        public int CreatedOrModifiedBy { get; set; }
    }
}
