using System;

namespace Business.Entities.LeaveTypeMaster
{
    public class LeaveTypeMaster
    {
        public int SrNo { get; set; }
        public int LeaveTypeID { get; set; }
        public string LeaveTypeText { get; set; }
        public bool IsActive { get; set; } = true;
        public int CreatedOrModifiedBy { get; set; }
    }
}
