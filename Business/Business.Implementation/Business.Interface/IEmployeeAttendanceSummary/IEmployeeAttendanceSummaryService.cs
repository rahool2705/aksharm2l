﻿using System.Data;
using System.Threading.Tasks;

namespace Business.Interface.IEmployeeAttendanceSummary
{
    public interface IEmployeeAttendanceSummaryService
    {
        public Task<DataSet> GetEmployeeAllAttendanceSummary(int employeeCategoryId = 0, int employeeId = 0, int month = 0, int year = 0);
    }
}
