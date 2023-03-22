using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Business.Entities.Employee
{
    public class EmployeePresent
    {
        public DateTime PresenceDateTime { get; set; } 
        public int CompanyID { get; set; }

        //Dravesh
        public int DepartmentID { get; set; }
        public string DepartmentText { get; set; }
        public string EmployeeName { get; set; }
        public int EmployeeTimeSheetMasterID { get; set; }
        public int CreatedOrModifiedBy { get; set; }
        public List<EmployeePresentList> getEmployeePresentList { get; set; }
    }


    public class EmployeePresentList
    {
        public int SrNo { get; set; }
        public int EmployeeTimeSheetDetailID { get; set; }
        public int EmployeeTimeSheetMasterID { get; set; }

        public int EmployeeID { get; set; }
        public int DepartmentID { get; set; }
        public DateTime PresenceDate { get; set; }
        public string EmployeeName { get; set; }
        public string DepartmentName { get; set; } 

        public DateTime InTime { get; set; }
        public DateTime OutTime { get; set; }

        [RegularExpression("^(?:0?[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage = "Invalid hours.")]
        public string BreakHour { get; set; }
        public string Overtime { get; set; }
    }
}
