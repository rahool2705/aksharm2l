using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Entities.Company
{
    public class CompanyDocument
    {
        public int CompanyDocumentsID { get; set; }
        public int CompanyID { get; set; }
        [NotMapped]
        public string EncryptedId { get; set; }

        [Required(ErrorMessage = "Enter Document Name")]
        public string DocumentName { get; set; }

        
        //public string DocumentDescription { get; set; }
        public string DocumentDesc { get; set; }
        public string DocumentExtension { get; set; }

        [Required(ErrorMessage = "Select Document Type")]
        public int DocumentTypeID { get; set; }
        public string DocumentPath { get; set; }
        public bool IsDeleted { get; set; }

        [Required(ErrorMessage = "Attach Document Image")]
        public IFormFile DocumentFile { get; set; }
        public int SrNo { get; set; }
        public bool IsActive { get; set; } = true;
        public int CreatedOrModifiedBy { get; set; }
    }
}
