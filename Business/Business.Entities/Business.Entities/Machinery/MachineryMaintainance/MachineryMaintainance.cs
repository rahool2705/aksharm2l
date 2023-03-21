using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Entities.Machinery.MachineryMaintainance
{
    public class MachineryMaintainance
    {
        public int MachineryMaintainanceID { get; set; }

        [Required(ErrorMessage = "Please Enter Machinery Name")]
        public string MachineryText { get; set; }

        [Required(ErrorMessage = "Please Enter Maintainance Purpose")]
        public string MaintainancePurpose { get; set; }

        [Required(ErrorMessage = "Please Enter Machinery Charge")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Quantity must be numeric")]
        public string Charges { get; set; }

        [Required(ErrorMessage = "Please Select Date")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Please Enter Remark")]
        public string Note { get; set; }

        public bool IsActive { get; set; } = true;

        public int CreatedOrModifiedBy { get; set; }
        public object SrNo { get; set; }
    }
}
