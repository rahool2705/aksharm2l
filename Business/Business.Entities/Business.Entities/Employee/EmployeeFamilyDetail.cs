using System.ComponentModel.DataAnnotations;

namespace Business.Entities.Employee
{
    public class EmployeeFamilyDetail
    {
        public int EmployeeFamilyDetailID { get; set; }
        public int EmployeeID { get; set; }
        [Required(ErrorMessage = "Select marital status")]
        public int MaritalStatusID { get; set; }
        [Required(ErrorMessage = "Select marital status")]
        public string MaritalStatusText { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string MotherName { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string FatherName { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string BrotherName { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string SisterName { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public int MotherBloodGroupID { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public int FatherBloodGroupID { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public int BrotherBloodGroupID { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public int SisterBloodGroupID { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public int WifeBloodGroupID { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string FatherContact { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string BrotherContact { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string SisterContact { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string MotherContact { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string WifeName { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string WifeContact { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public int NoofChild { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public int NoofBikeScooty { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public int NoofCar { get; set; }

        [Required(ErrorMessage = "Emergency Mobile Number is required")]
        public string EmergencyMobileNumber { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string WhatsAppNo { get; set; }
        public int CreatedModifiedBy { get; set; }
    }
}
