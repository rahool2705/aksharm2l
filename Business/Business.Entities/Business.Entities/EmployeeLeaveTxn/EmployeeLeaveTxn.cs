using System;

namespace Business.Entities.EmployeeLeaveTxn
{
    public class EmployeeLeaveTxn
    {
        public int EmployeeLeaveTxnID { get; set; }
        public int EmployeeID { get; set; }
        public int LeaveTypeID { get; set; }
        public DateTime LeaveStartDate { get; set; }
        public DateTime LeaveEndDate { get; set; }
        public decimal NoOfDays { get; set; }
        public string Reason { get; set; }
        public int CreatedOrModifiedBy { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsCancel { get; set; }
        public int FinYearID { get; set; }
        public int SrNo { get; set; }
        public string LeaveTypeText { get; set; }
        public string FinancialYear { get; set; }
        public string EmployeeName { get; set; }
    }
}
