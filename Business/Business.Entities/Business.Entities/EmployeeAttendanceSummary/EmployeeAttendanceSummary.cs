using System.Collections.Generic;

namespace Business.Entities.EmployeeAttendanceSummary
{
    public class EmployeeAttendanceSummary
    {
        public int a { get; set; }
        public int EID { get; set; }
        public int EName { get; set; }
        public int Desc { get; set; }
        public List<Date> Dates { get; set; }
    }
    public class Date
    {
        public string MonthDate { get; set; }
    }
}
