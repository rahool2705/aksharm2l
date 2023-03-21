using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace Business.Entities.WagesConfig
{
    public class WagesConfig
    {
        public int SrNo { get; set; }
        public int WagesConfigID { get; set; }
        public int HRConfigID { get; set; }
        public int EmployeeCategoryID { get; set; }
        public string EmployeeCategoryText { get; set; }
        //^[A-Za-z0-9_@./#&+-]*$

        [RegularExpression("^[0-9.]*$", ErrorMessage = "Minimum Wages be in number.")]
        public decimal? MinimumWages { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "Special Allowance be in number.")]
        public decimal? SpecialAllowance { get; set; }
        public decimal? TotalWagesPerDay { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? EntryDate { get; set; }
        public bool IsActive { get; set; } = true;
        public int CreatedOrModifiedBy { get; set; }
    }
}
