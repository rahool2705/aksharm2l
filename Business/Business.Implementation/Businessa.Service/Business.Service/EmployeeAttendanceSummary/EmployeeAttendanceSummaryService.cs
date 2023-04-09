using Business.Interface.IEmployeeAttendanceSummary;
using Business.SQL;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;
using System;
using Business.Entities.Employee;
using Business.Entities.Master.EmployeeCategory;
using Business.Entities.Master.EmployementType;
using System.ComponentModel.Design;

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

        public async Task<DataSet> GetEmployeeAllAttendanceSummary(int employeeCategoryId = 0, int userId = 0, int month = 0, int year = 0, int departmentId = 0, string searchString = "")
        {
            DataTable table = new DataTable();
            //int totalItemCount = 0;
            //PagedDataTable<Entities.EmployeeAttendanceSummary.EmployeeAttendanceSummary> lst = null;
            try
            {
                SqlParameter[] param = {
                        new SqlParameter("@EmployeeCategoryId",employeeCategoryId )
                        ,new SqlParameter("@UserID", userId)
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

        #region Employee Salary Summuary

        public async Task<DataSet> GetEmployeeSalarySummary(int employeeCategoryId = 0, int userId = 0, int companyId = 0, int month = 0, int year = 0, int employeeId = 0)
        {
            DataTable table = new DataTable();
            //int totalItemCount = 0;
            //PagedDataTable<Entities.EmployeeAttendanceSummary.EmployeeAttendanceSummary> lst = null;
            try
            {
                SqlParameter[] param = {
                        new SqlParameter("@EmployeeCategoryId",employeeCategoryId )
                        ,new SqlParameter("@UserID", userId)
                        ,new SqlParameter("@Month", month)
                        ,new SqlParameter("@Year", year)
                        ,new SqlParameter("@EmployeeID", employeeId)
                        ,new SqlParameter("@CompanyID", companyId)
                        };
                DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "UDF_Get_EmployeeSalaryDetail", param);
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

        public async Task<DataSet> ProcesSalary(int year, int month, int companyId, int employmentTypeId, int employeeCategoryId, int userId,  DateTime salaryDate)
        {
            
            try
            {
                SqlParameter[] param = {
                    new SqlParameter("@YR", year)
                    ,new SqlParameter("@MTH", month)
                    ,new SqlParameter("@CompanyID", companyId)
                    ,new SqlParameter("@EmploymentTypeID", employmentTypeId)
                    ,new SqlParameter("@EmployeeCategoryId", employeeCategoryId)
                    ,new SqlParameter("@UserID", userId)
                    ,new SqlParameter("@SalaryDate", salaryDate)
                };

                DataSet  salaryProcess = (DataSet)await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "USP_RunSalaryProcess", param);
                return salaryProcess;
                
            }
            catch
            {
                throw;
            }
        }

        #endregion Employee Salary Summuary
<<<<<<< Updated upstream
=======


        public async Task<int> VerifyEmplyeeSalary(int year, int month, int companyId, int employeeId, int employeeCategoryId, int userId)
        {
            try
            {
                SqlParameter[] param = {
                    new SqlParameter("@Year", year)
                    ,new SqlParameter("@Month", month)
                    ,new SqlParameter("@CompanyID", companyId)
                    ,new SqlParameter("@EmployeeID", employeeId)
                    ,new SqlParameter("@EmployeeCategoryId", employeeCategoryId)
                    //,new SqlParameter("@IsDeleted", employeeDocument.IsDeleted)
                    ,new SqlParameter("@UserID", userId)
                    ,new SqlParameter("@IsVerified", true)
                };

                var obj = await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "USP_Insert_VerifiedEmployeeSalary", param);

                return obj != null ? Convert.ToInt32(obj) : 0;
            }
            catch
            {
                throw;
            }
        }

        public async Task<DataSet> GetEmployeeSalaryDetail(int year, int month, int companyId, int employeeId, int employeeCategoryId,int userId)
        {
            DataTable table = new DataTable();
            //int totalItemCount = 0;
            //PagedDataTable<Entities.EmployeeAttendanceSummary.EmployeeAttendanceSummary> lst = null;
            try
            {
                SqlParameter[] param = {
                        new SqlParameter("@EmployeeCategoryId",employeeCategoryId )
                        ,new SqlParameter("@UserID", userId)
                        ,new SqlParameter("@Month", month)
                        ,new SqlParameter("@Year", year)
                        ,new SqlParameter("@EmployeeID", employeeId)
                        ,new SqlParameter("@CompanyID", companyId)
                        };
                DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "UDF_Get_EmployeeSalaryDetailInHorizontal", param);
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
>>>>>>> Stashed changes
    }
}
