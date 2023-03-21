using Microsoft.AspNetCore.Http;
using System;

namespace Business.Entities.Customer
{
    public class CustomerDocument
    {
        public int SrNo { get; set; }
        public int CustomerDocumentID { get; set; }
        public int CustomerID { get; set; }
        public string DocumentName { get; set; }
        public string DocumentDescription { get; set; }
        public string DocumentExtension { get; set; }
        public int DocumentTypeID { get; set; }
        public string DocumentPath { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; } = true;
        public int CreatedOrModifiedBy { get; set; }
        public IFormFile DocumentFile { get; set; }
    }
}
