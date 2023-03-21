using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Business.Entities.Lead
{
    public class LeadDocument
    {
        
        public int LeadDocumentID { get; set; }
        public int LeadID { get; set; }

        [Required(ErrorMessage = "Enter Document Name")]
        public string DocumentName { get; set; }

        [Required(ErrorMessage = "Enter Document Description")]
        public string DocumentDescription { get; set; }
        public string DocumentExtension { get; set; }

        [Required(ErrorMessage = "Select Document Type")]
        public int DocumentTypeID { get; set; }
        public string DocumentPath { get; set; }
        public bool IsDeleted { get; set; }       
        public IFormFile DocumentFile { get; set; }
        public int SrNo { get; set; }
        public bool IsActive { get; set; } = true;
        public int CreatedOrModifiedBy { get; set; }
    }
}
