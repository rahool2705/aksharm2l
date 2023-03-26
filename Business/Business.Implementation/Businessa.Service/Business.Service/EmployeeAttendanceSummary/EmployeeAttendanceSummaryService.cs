using Business.Interface.IEmployeeAttendanceSummary;
using Business.SQL;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;
using System;

namespace Business.Service.EmployeeAttendanceSummary
{
    public class EmployeeAttendanceSummaryService : IEmployeeAttendanceSummaryService
    {
        private IConfiguration _config { get; set; }
        private string connection = string.Empty;
        public EmployeeAttendanceSummaryService(IConfiguration config)
        {
            _config = config;
            connection = _config.GetConnectionString("DefaultConnection");
        }

        public async Task<DataSet> GetEmployeeAllAttendanceSummary(int employeeCategoryId = 0, int employeeId = 0, int month = 0, int year = 0, int departmentId = 0, string searchString = "")
        {
            DataTable table = new DataTable();
            //int totalItemCount = 0;
            //PagedDataTable<Entities.EmployeeAttendanceSummary.EmployeeAttendanceSummary> lst = null;
            try
            {
                SqlParameter[] param = {
                        new SqlParameter("@EmployeeCategoryId",employeeCategoryId )
                        ,new SqlParameter("@EmployeeID", employeeId)
                        ,new SqlParameter("@Month", month)
                        ,new SqlParameter("@Year", year)
                        ,new SqlParameter("@SearchString", searchString)
                        ,new SqlParameter("@DepartmentID", departmentId)
                        };
                DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_Report_EmployeeAttendanceSummary", param);
                //{
                //    if (ds.Tables.Count > 0)
                //    {
                //        table = ds.Tables[0];
                //        if (table.Rows.Count > 0)
                //        {
                //            if (table.ContainColumn("TotalCount"))
                //                totalItemCount = Convert.ToInt32(table.Rows[0]["TotalCount"]);
                //            else
                //                totalItemCount = table.Rows.Count;
                //        }
                //    }
                    //lst = table.ToPagedDataTableList<Entities.EmployeeAttendanceSummary.EmployeeAttendanceSummary>(1, 1000, totalItemCount);
                    return ds;
                //}
            }
            catch
            {
                throw;
            }
            finally
            {
                if (table != null)
                    table.Dispose();
            }
        }

        public async Task<DataSet> GetEmployeeAllDetailSummary(int employeecategoryId = 0, int departmentId = 0, string searchstring = null)
        {
            DataTable table = new DataTable();
            //int totalItemCount = 0;
            //PagedDataTable<Entities.EmployeeAttendanceSummary.EmployeeAttendanceSummary> lst = null;
            try
            {
                SqlParameter[] param = {
                        new SqlParameter("@EmployeeCategoryId",employeecategoryId )
                        ,new SqlParameter("@DepartmentID", departmentId)
                        ,new SqlParameter("@SearchString", searchstring)

                        };
                DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_Report_EmployeeDetailSummary", param);
                //{
                //    if (ds.Tables.Count > 0)
                //    {
                //        table = ds.Tables[0];
                //        if (table.Rows.Count > 0)
                //        {
                //            if (table.ContainColumn("TotalCount"))
                //                totalItemCount = Convert.ToInt32(table.Rows[0]["TotalCount"]);
                //            else
                //                totalItemCount = table.Rows.Count;
                //        }
                //    }
                //lst = table.ToPagedDataTableList<Entities.EmployeeAttendanceSummary.EmployeeAttendanceSummary>(1, 1000, totalItemCount);
                return ds;
                //}
            }
            catch
            {
                throw;
            }
            finally
            {
                if (table != null)
                    table.Dispose();
            }
        }
    }
}
