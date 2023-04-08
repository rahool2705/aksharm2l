namespace Business.Entities.WorkingDaysModel
{
    public class WorkingDay
    {
        public int WorkCalendarID { get; set; }
        public int? Year { get; set; }
        public int Month { get; set; }
        public int? WorkingDays { get; set; }
        public int? Holidays { get; set; }
        public bool IsActive { get; set; } = true;
        public int CreatedOrModifiedBy { get; set; }
        public object SrNo { get; set; }
    }
}
