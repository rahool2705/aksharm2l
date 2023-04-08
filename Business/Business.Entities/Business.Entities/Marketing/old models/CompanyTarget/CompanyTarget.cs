using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Entities.Marketing.CompanySale
{
    public class CompanyTarget
    {
        [Required(ErrorMessage = "Select company name")]
        public int CompanyTargetID { get; set; }
                
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Select start date")]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Select end date")]
        public DateTime EndDate { get; set; }
        [Required(ErrorMessage = "Enter company target value")]
        public decimal TargetValue { get; set; }
        [Required(ErrorMessage = "Financial year cannot be null")]
        public int FinancialYearID { get; set; }
        public string FinancialYear { get; set; }        
        public bool IsActive { get; set; } = true;
        public int CreatedOrModifiedBy { get; set; }
        public object SrNo { get; set; }
        public int CompanyID { get; set; }
        public string CompanyName { get; set; }


    }
}
