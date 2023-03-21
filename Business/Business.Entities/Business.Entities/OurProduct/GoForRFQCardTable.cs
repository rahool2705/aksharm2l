using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Entities.OurProduct
{
    public class GoForRFQCardTable
    {
        public int InquiryID { get; set; }
        public int ItemForRFQID { get; set; }
        public string ItemDescription { get; set; }
        public string Item { get; set; }
        public int UOMID { get; set; }
        public string UOMText { get; set; }

        [RegularExpression("^[0-9]*$", ErrorMessage = "UPRN must be numeric")]
        public string Quantity { get; set; }
        public string Remark { get; set; }
        public bool IsActive { get; set; } = true;
        public int CreatedOrModifiedBy { get; set; }
        public int SrNo { get; set; }
        public int ItemID { get; set; }
        public bool IsInspectionRequired { get; set; }
        public int InspectionAgencyID { get; set; }
        public int InquiryItemID { get; set; }
        
    }
}
