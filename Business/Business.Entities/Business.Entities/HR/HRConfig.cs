using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Entities.HR
{

    public class HRConfig
    {
        public int HRConfigID { get; set; }
        public int EmployeeCategoryId { get; set; }

        public string EmployeeCategoryText { get; set; }
     
        public int? Year { get; set; }
        public int? WorkingDayInMonth { get; set; }
        public int? WorkingHrsInDay { get; set; }
        public int? OTPaymentIn { get; set; }
        public string WeekOff1 { get; set; }
        public string WeekOff2 { get; set; }
        public decimal InterestOnLoan { get; set; }
        public bool IsActive { get; set; }
        public int CreatedOrModifiedBy { get; set; }
     
    }


}
