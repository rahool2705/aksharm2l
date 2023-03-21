using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Entities.Contractor
{
    public class LeadDocument
    {
        
        public int ContractorDocumentID { get; set; }
        public int ContractorID { get; set; }
        [NotMapped]
        public string EncryptedId { get; set; }

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
