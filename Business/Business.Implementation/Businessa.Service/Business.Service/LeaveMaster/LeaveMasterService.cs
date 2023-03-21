using Business.Interface.ILeaveMaster;
using Business.SQL;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;
using System;
using Business.Entities.LeaveTypeMaster;
using Business.Entities.EmployeeLeaveTxn;

namespace Business.Service.LeaveMaster
{
    public class LeaveMasterService : ILeaveMasterService
    {
        private IConfiguration _config { get; set; }
        private string connection = string.Empty;
        public LeaveMasterService(IConfiguration config)
        {
            _config = config;
            connection = _config.GetConnectionString("DefaultConnection");
        }

        #region Leave type master
        public async Task<PagedDataTable<LeaveTypeMaster>> GetAllLeaveTypeMaster(int pageNo = 1, int pageSize = 10, string searchString = "", string orderBy = "EmployeeID", string sortBy = "ASC")
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

                using (DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_GetAll_LeaveTypeMaster", param))
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
                    PagedDataTable<LeaveTypeMaster> lst = table.ToPagedDataTableList<LeaveTypeMaster>
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


        public async Task<LeaveTypeMaster> GetLeaveTypeAsync(int leaveTypeId)
        {
            LeaveTypeMaster result = null;
            try
            {
                SqlParameter[] param = { new SqlParameter("@LeaveTypeID", leaveTypeId) };
                DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_Get_LeaveTypeMaster", param);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            result = dr.ToPagedDataTableList<LeaveTypeMaster>();
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

        public async Task<int> AddUpdateLeaveType(LeaveTypeMaster leaveTypeMaster)
        {
            try
            {
                SqlParameter[] param = {
                    new SqlParameter("@LeaveTypeID",leaveTypeMaster.LeaveTypeID),
                new SqlParameter("@LeaveTypeText", leaveTypeMaster.LeaveTypeText ),
                new SqlParameter("@CreatedOrModifiedBy", leaveTypeMaster.CreatedOrModifiedBy),
                new SqlParameter("@IsActive", leaveTypeMaster.IsActive )
                };

                var obj = await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_IU_LeaveTypeMaster", param);

                return obj != null ? Convert.ToInt32(obj) : 0;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion Leave type master

        #region Employee leave transaction

        public async Task<PagedDataTable<EmployeeLeaveTxn>> GetAllEmployeeLeaveTxn(int pageNo = 1, int pageSize = 10, string searchString = "", string orderBy = "SrNo", string sortBy = "ASC")
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

                using (DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_GetAll_EmployeeLeaveTxn", param))
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
                    PagedDataTable<EmployeeLeaveTxn> lst = table.ToPagedDataTableList<EmployeeLeaveTxn>
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


        public async Task<EmployeeLeaveTxn> GetEmployeeLeaveTxnAsync(int employeeLeaveTxnID, int empliyeeId)
        {
            EmployeeLeaveTxn result = null;
            try
            {
                SqlParameter[] param = {
                    new SqlParameter("@EmployeeLeaveTxnID", employeeLeaveTxnID),
                    new SqlParameter("@EmployeeID", empliyeeId)

                };
                DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_Get_EmployeeLeaveTxn", param);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            result = dr.ToPagedDataTableList<EmployeeLeaveTxn>();
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

        public async Task<int> AddUpdateEmployeeLeaveTxn(EmployeeLeaveTxn employeeLeaveTxn)
        {
            try
            {
                SqlParameter[] param = {
                      new SqlParameter("@EmployeeLeaveTxnID", employeeLeaveTxn.EmployeeLeaveTxnID )
                      ,new SqlParameter("@EmployeeID", employeeLeaveTxn.EmployeeID )
                      ,new SqlParameter("@LeaveTypeID", employeeLeaveTxn.LeaveTypeID )
                      ,new SqlParameter("@LeaveStartDate", employeeLeaveTxn.LeaveStartDate )
                      ,new SqlParameter("@LeaveEndDate", employeeLeaveTxn.LeaveEndDate )
                      ,new SqlParameter("@NoOfDays", employeeLeaveTxn.NoOfDays )
                      ,new SqlParameter("@Reason", employeeLeaveTxn.Reason )
                      ,new SqlParameter("@CreatedOrModifiedBy", employeeLeaveTxn.CreatedOrModifiedBy )
                      ,new SqlParameter("@IsCancel", employeeLeaveTxn.IsCancel )
                      ,new SqlParameter("@FinYearID", employeeLeaveTxn.FinYearID )
                };

                var obj = await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_IU_EmployeeLeaveTxn", param);

                return obj != null ? Convert.ToInt32(obj) : 0;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> CancelEmployeeLeave(EmployeeLeaveTxn employeeLeaveTxn)
        {
            try
            {
                SqlParameter[] param = {
                    new SqlParameter("@EmployeeLeaveTxnID", employeeLeaveTxn.EmployeeLeaveTxnID)
                    ,new SqlParameter("@EmployeeID", employeeLeaveTxn.EmployeeID)
                    ,new SqlParameter("@CreatedOrModifiedBy", employeeLeaveTxn.CreatedOrModifiedBy)
                    ,new SqlParameter("@IsCancel", employeeLeaveTxn.IsCancel)
                };

                var obj = await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_U_EmployeeLeaveIsCancel", param);

                return obj != null ? Convert.ToInt32(obj) : 0;
            }
            catch
            {
                throw;
            }
        }
        #endregion Employee leave transaction
    }
}
