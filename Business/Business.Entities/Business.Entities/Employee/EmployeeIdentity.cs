using System.ComponentModel.DataAnnotations;

namespace Business.Entities.Employee
{
    public class EmployeeIdentity
    {
        public int SrNo { get; set; }
        public int EmployeeID { get; set; }
        public int EmployeeIdentityID { get; set; }

        [Required(ErrorMessage = "Please select identity proof")]
        /*[RegularExpression("^[ A-Za-z-]*$", ErrorMessage = "Employee category name must be charater only")]*/
        public int IdentityProofTypeID { get; set; }        
        public string IdentityProofTypeText { get; set; }

        [Required(ErrorMessage = "Please enter identity proof value")]
        public string IdentityProofCode { get; set; }
        public bool IsActive { get; set; }=true; 
        public int CreatedOrModifiedBy { get; set; }
    }
}
