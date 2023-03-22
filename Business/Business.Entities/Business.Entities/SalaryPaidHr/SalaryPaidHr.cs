using System;

namespace Business.Entities.SalaryPaidHr
{
    public class SalaryPaidHr
    {
        public int SalaryPaidHrID { get; set; }
        public int EmployeeID { get; set; }
        public int? ActualHours { get; set; }
        public int? AdjustmentHour { get; set; }
        public int CreatedOrModifiedBy { get; set; }
        public int actualhourUserID { get; set;}
        public int adjustmenthourUserID { get; set;}
    }
}
