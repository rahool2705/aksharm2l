using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Entities.OurProduct
{
    public class GoForRFQCard
    {
        public int ItemRFQCardID { get; set; }

        [Required (ErrorMessage ="Select the Customer")]
        public int CustomerID { get; set; }
        public string ItemCollectionID { get; set; }
        public string CustomerName { get; set; }
        public DateTime CustomerInquiryDate { get; set; }
        public string InquiryNo { get; set; }

        [Required(ErrorMessage = "Select the Inquiry Date")]
        public DateTime InquiryDate { get; set; }
        public string Mediator { get; set; }

        [Required(ErrorMessage = "Select the Finacial Year")]
        public int FinYearID { get; set; }
        public string FinYearText { get; set; }
        public string InquiredBy { get; set; }

        
        public int InquiryID { get; set; }
        [Required(ErrorMessage = "Select the Inquiry Type")]
        public string InquiryType { get; set; }

        
        public int InquiryTypeID { get; set; }
        public int QuotationDueDate { get; set; }
        public string Remark { get; set; }
        public bool IsActive { get; set; } = true;
        public int CreatedOrModifiedBy { get; set; }
        public int SrNo { get; set; }
        public object GoForRFQCardTable { get; set; }
        public List<GoForRFQCardTable> LstGoForRFQCardTable { get; set; }
    }
}
