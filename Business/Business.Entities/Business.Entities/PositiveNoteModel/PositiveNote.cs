using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Entities.PositiveNoteModel
{
    public class PositiveNote
    {
        public int PositiveNoteID { get; set; }

        [Required(ErrorMessage = "Enter the note")]
        public string PositiveNoteText { get; set; }

        [Required(ErrorMessage = "Enter Remark")]
        public string Remark { get; set; }
        public bool IsActive { get; set; } = true;
        public int CreatedOrModifiedBy { get; set; }
        public object SrNo { get; set; }
    }
}
