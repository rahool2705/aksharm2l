using System.ComponentModel.DataAnnotations;

namespace Business.Entities.OurProduct
{
    public class OurProduct
    {
        public int ItemForRFQID { get; set; }
        public int ItemForRFQText { get; set; }
        public string guidColID { get; set; }
        public string ItemCollectionID { get; set; }
        public int ItemCollectionText { get; set; }
        public bool IsRFQDone { get; set; }
        public string Item { get; set; }
        //public string ItemText { get; set; }
        [Required(ErrorMessage = "Select the Units Type")]
        public int UOMID { get; set; }
        public string UOMText { get; set; }
        [Required(ErrorMessage = "Enter numbers of Quantity")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Quantity must be numeric")]
        public string Quantity { get; set; }
        public string ItemDescription { get; set; }
        public int CreatedOrModifiedBy { get; set; }
        public int SrNo { get; set; }
    }
}
