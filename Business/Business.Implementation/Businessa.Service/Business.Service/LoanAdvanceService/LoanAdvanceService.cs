using Business.Entities.CanteenCharges;
using Business.Interface.ILoanAdvanceService;
using Business.SQL;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;
using System;
using Business.Entities.EmployeeLoan;
using Business.Entities.EmployeeAdvances;

namespace Business.Service.LoanAdvanceService
{
    public class LoanAdvanceService : ILoanAdvanceService
    {
        private IConfiguration _config { get; set; }
        private string connection = string.Empty;
        public LoanAdvanceService(IConfiguration config)
        {
            _config = config;
            connection = _config.GetConnectionString("DefaultConnection");
        }

        #region Employee Loan
        public async Task<PagedDataTable<EmployeeLoan>> GetAllEmployeeLoanAsync(int pageNo = 1, int pageSize = 20, string searchString = "", string orderBy = "SrNo", string sortBy = "ASC")
        {
            DataTable table = new DataTable();
            int totalItemCount = 0;
            try
            {
                SqlParameter[] param = {
                        new SqlParameter("@PageNo",pageNo)
                        ,new SqlParameter("@PageSize",pageSize)
                        ,new SqlParameter("@SearchString",searchString)
                        ,new SqlParameter("@OrderBy",orderBy)
                        ,new SqlParameter("@SortBy",sortBy=="ASC"?0:1)
                        };

                using (DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_GetAll_EmployeeLoan", param))
                {
                    if (ds.Tables.Count > 0)
                    {
                        table = ds.Tables[0];
                        if (table.Rows.Count > 0)
                        {
                            if (table.ContainColumn("TotalCount"))
                                totalItemCount = Convert.ToInt32(table.Rows[0]["TotalCount"]);
                            else
                                totalItemCount = table.Rows.Count;
                        }
                    }
                    PagedDataTable<EmployeeLoan> lst = table.ToPagedDataTableList<EmployeeLoan>
                        (pageNo, pageSize, totalItemCount, searchString, orderBy, sortBy);
                    return lst;
                }
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

        public async Task<EmployeeLoan> GetEmployeeLoanAsync(int employeeLoanID)
        {
            EmployeeLoan result = null;
            try
            {
                SqlParameter[] param = { new SqlParameter("@EmployeeLoanID", employeeLoanID) };
                DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_Get_EmployeeLoan", param);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            result = dr.ToPagedDataTableList<EmployeeLoan>();
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> EmployeeLoanCreateOrUpdateAsync(EmployeeLoan model)
        {
            try
            {
                SqlParameter[] param = {
                    new SqlParameter("@EmployeeLoanID", model.EmployeeLoanID )
                    ,new SqlParameter("@EmployeeID", model.EmployeeID )
                    ,new SqlParameter("@PaymentDate", model.PaymentDate )
                    ,new SqlParameter("@EmployeeLoanAmount", model.EmployeeLoanAmount )
                    ,new SqlParameter("@TenureMonths", model.TenureMonths )
                    ,new SqlParameter("@InterestRate", model.InterestRate )
                    ,new SqlParameter("@AdjustmentAmount", model.AdjustmentAmount )
                    ,new SqlParameter("@IsActive", model.IsActive )
                    ,new SqlParameter("@CreatedOrModifiedBy", model.CreatedOrModifiedBy )

                };

                var obj = await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_IU_EmployeeLoan", param);

                return obj != null ? Convert.ToInt32(obj) : 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion Employee Loan

        #region Employee Advance

        public async Task<PagedDataTable<EmployeeAdvances>> GetAllEmployeeAdvancesAsync(int pageNo = 1, int pageSize = 20, string searchString = "", string orderBy = "SrNo", string sortBy = "ASC")
        {
            DataTable table = new DataTable();
            int totalItemCount = 0;
            try
            {
                SqlParameter[] param = {
                        new SqlParameter("@PageNo",pageNo)
                        ,new SqlParameter("@PageSize",pageSize)
                        ,new SqlParameter("@SearchString",searchString)
                        ,new SqlParameter("@OrderBy",orderBy)
                        ,new SqlParameter("@SortBy",sortBy=="ASC"?0:1)
                        };

                using (DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_GetAll_EmployeeAdvances", param))
                {
                    if (ds.Tables.Count > 0)
                    {
                        table = ds.Tables[0];
                        if (table.Rows.Count > 0)
                        {
                            if (table.ContainColumn("TotalCount"))
                                totalItemCount = Convert.ToInt32(table.Rows[0]["TotalCount"]);
                            else
                                totalItemCount = table.Rows.Count;
                        }
                    }
                    PagedDataTable<EmployeeAdvances> lst = table.ToPagedDataTableList<EmployeeAdvances>
                        (pageNo, pageSize, totalItemCount, searchString, orderBy, sortBy);
                    return lst;
                }
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

        public async Task<EmployeeAdvances> GetEmployeeAdvancesAsync(int employeeAdvancesID)
        {
            EmployeeAdvances result = null;
            try
            {
                SqlParameter[] param = { new SqlParameter("@EmployeeAdvancesID", employeeAdvancesID) };
                DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_Get_EmployeeAdvances", param);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            result = dr.ToPagedDataTableList<EmployeeAdvances>();
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<int> EmployeeAdvancesCreateOrUpdateAsync(EmployeeAdvances model)
        {
            try
            {
                SqlParameter[] param = {
                    new SqlParameter("@EmployeeAdvancesID", model.EmployeeAdvancesID)
                    ,new SqlParameter("@EmployeeID", model.EmployeeID)
                    ,new SqlParameter("@PaymentDate", model.PaymentDate)
                    ,new SqlParameter("@Amount", model.Amount)
                    ,new SqlParameter("@Description", model.Description)
                    ,new SqlParameter("@IsActive", model.IsActive)
                    ,new SqlParameter("@CreatedOrModifiedBy", model.CreatedOrModifiedBy)

                };

                var obj = await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_IU_EmployeeAdvances", param);

                return obj != null ? Convert.ToInt32(obj) : 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion Employee Advance
    }
}
