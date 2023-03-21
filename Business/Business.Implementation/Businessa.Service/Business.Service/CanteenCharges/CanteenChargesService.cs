using Business.Interface.CanteenCharges;
using Business.SQL;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;
using System;
using Business.Entities.CanteenCharges;
using Business.Entities.WagesConfig;

namespace Business.Service
{
    public class CanteenChargesService : ICanteenChargesService
    {
        private IConfiguration _config { get; set; }
        private string connection = string.Empty;
        public CanteenChargesService(IConfiguration config)
        {
            _config = config;
            connection = _config.GetConnectionString("DefaultConnection");
        }
        public async Task<PagedDataTable<CanteenCharges>> GetAllCanteenChargesAsync(int pageNo = 1, int pageSize = 20, string searchString = "", string orderBy = "SrNo", string sortBy = "ASC")
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

                using (DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_GetAll_CanteenCharges", param))
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
                    PagedDataTable<CanteenCharges> lst = table.ToPagedDataTableList<CanteenCharges>
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

        public async Task<CanteenCharges> GetCanteenChargesAsync(int canteenChargesID)
        {
            CanteenCharges result = null;
            try
            {
                SqlParameter[] param = { new SqlParameter("@CanteenChargesID", canteenChargesID) };
                DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_Get_CanteenCharges", param);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            result = dr.ToPagedDataTableList<CanteenCharges>();
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

        public async Task<int> CanteenChargesCreateOrUpdateAsync(CanteenCharges model)
        {
            try
            {
                SqlParameter[] param = {
                    new SqlParameter("@CanteenChargesID", model.CanteenChargesID)
                    ,new SqlParameter("@EmployeeID", model.EmployeeID)
                    ,new SqlParameter("@Month", model.Month)
                    ,new SqlParameter("@Year", model.Year)
                    ,new SqlParameter("@BreakfastQuantity", model.BreakfastQuantity)
                    ,new SqlParameter("@BreakfastRate", model.BreakfastRate)
                    ,new SqlParameter("@LunchQuantity", model.LunchQuantity)
                    ,new SqlParameter("@LunchRate", model.LunchRate)
                    ,new SqlParameter("@DinnerQuantity", model.DinnerQuantity)
                    ,new SqlParameter("@DinnerRate", model.DinnerRate)
                    ,new SqlParameter("@IsActive", model.IsActive)
                    ,new SqlParameter("@CreatedOrModifiedBy", model.CreatedOrModifiedBy)

                };

                var obj = await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_IU_CanteenCharges", param);

                return obj != null ? Convert.ToInt32(obj) : 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
