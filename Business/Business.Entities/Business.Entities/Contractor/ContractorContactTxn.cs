using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace Business.Entities.Contractor
{
    public class ContractorContactTxn
    {
        public int ContractorContactID { get; set; }
        public int ContractorID { get; set; }
        public string EncryptedId { get; set; }

        public string Prefix { get; set; }
        
        [Required(ErrorMessage = "Contact person name is required.")]
        public string? ContactPersonName { get; set; }
        [Required(ErrorMessage = "Please select Designation.")]
        public int DesignationID { get; set; }
        [Required(ErrorMessage = "Please select Department.")]
        public int DepartmentID { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }        
        
        [Required(ErrorMessage = "Personal mobile number is required..")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Mobile number must be numberic")]
        public string? PersonalMobile { get; set; }
        /*[Required(ErrorMessage = "Office mobile number is required..")]*/
        public string OfficeMobile { get; set; }
        public string PersonalEmailID { get; set; }
        /*[Required(ErrorMessage = "Office emailid is required..")]*/
        public string OfficeEmailID { get; set; }
        public string AlternativeMobile { get; set; }
        public string EmailGroupName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Religion { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsResigned { get; set; }
        public string Notes { get; set; }
        public int SrNo { get; set; }
        public string LogoImagePath { get; set; }

        
        /*public string UnitNoName { get; set; }
        public string ContractorCode { get; set; }
        public string ContractorName { get; set; }
        public string GroupName { get; set; }
        public string OwnerName { get; set; }
        public string ContactPhone1 { get; set; }
        public string Mobile1 { get; set; }
        public string FaxNo { get; set; }
        public int? IndustryTypeID { get; set; }
        public int? BusinessTypeID { get; set; }
        public IFormFile LogoImage { get; set; }
        public string LogoImageName { get; set; }
        public int? CreatedOrModifiedBy { get; set; }*/

    }
}
