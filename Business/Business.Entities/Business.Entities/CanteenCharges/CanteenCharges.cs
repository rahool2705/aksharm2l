using System.ComponentModel.DataAnnotations;

namespace Business.Entities.CanteenCharges
{
    public class CanteenCharges
    {
        public int SrNo { get; set; }
        public int CanteenChargesID { get; set; }
        [Required(ErrorMessage ="Please select Employee.")]
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }


        [RegularExpression("^[0-9]*$", ErrorMessage = "Month be in number.")]
        public int? Month { get; set; }


        [RegularExpression("^[0-9]*$", ErrorMessage = "Year be in number.")]
        public int? Year { get; set; }


        [RegularExpression("^[0-9]*$", ErrorMessage = "Breakfast Quantity be in number.")]
        public int? BreakfastQuantity { get; set; }


        [RegularExpression("^[0-9.]*$", ErrorMessage = "Breakfast Rate be in number.")]
        public decimal? BreakfastRate { get; set; }


        [RegularExpression("^[0-9]*$", ErrorMessage = "Lunch Quantity be in number.")]
        public int? LunchQuantity { get; set; }


        [RegularExpression("^[0-9.]*$", ErrorMessage = "Lunch Rate be in number.")]
        public decimal? LunchRate { get; set; }


        [RegularExpression("^[0-9]*$", ErrorMessage = "Dinner Quantity be in number.")]
        public int? DinnerQuantity { get; set; }


        [RegularExpression("^[0-9.]*$", ErrorMessage = "Dinner Rate be in number.")]
        public decimal? DinnerRate { get; set; }


        public bool IsActive { get; set; } = true;
        public int CreatedOrModifiedBy { get; set; }
    }
}
