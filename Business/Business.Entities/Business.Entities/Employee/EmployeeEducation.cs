using System;

namespace Business.Entities.Employee
{
    public class EmployeeEducation
    {
        public int SrNo { get; set; }
        public int EmployeeEducationID { get; set; }
        public int EmployeeID { get; set; }
        public string SchoolOrUniversity { get; set; }
        public string Degree { get; set; }
        public string FieldOfStudy { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Grade { get; set; }
        public string ActivitiesAndSocialities { get; set; }
        public string Description { get; set; }
        public bool IsCurrentEducation { get; set; }
        public int CreatedOrModifiedBy { get; set; }
    }
}
