using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Entities.VenueTypeModel
{
    public class VenueType
    {
        public int VenueTypeID { get; set; }
        [Required(ErrorMessage = "Please Enter Venue Type Name")]
        public string VenueTypeText { get; set; }
        public bool IsActive { get; set; } = true;
        public int CreatedOrModifiedBy { get; set; }
        public object SrNo { get; set; }
    }
}
