using Business.Entities.WagesConfig;
using Business.Interface.IWagesConfig;
using Business.SQL;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;
using System;

namespace Business.Service.WagesConfigService
{
    public class WagesConfigService : IWagesConfigService
    {
        private IConfiguration _config { get; set; }
        private string connection = string.Empty;
        public WagesConfigService(IConfiguration config)
        {
            _config = config;
            connection = _config.GetConnectionString("DefaultConnection");
        }
        public async Task<PagedDataTable<WagesConfig>> GetAllWagesConfigAsync(int pageNo = 1, int pageSize = 20, string searchString = "", string orderBy = "SrNo", string sortBy = "ASC")
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

                using (DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_GetAll_WagesConfig", param))
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
                    PagedDataTable<WagesConfig> lst = table.ToPagedDataTableList<WagesConfig>
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


        public async Task<WagesConfig> GetWagesConfigAsync(int wagesConfigID)
        {
            WagesConfig result = null;
            try
            {
                SqlParameter[] param = { new SqlParameter("@WagesConfigID", wagesConfigID) };
                DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_Get_WagesConfig", param);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            result = dr.ToPagedDataTableList<WagesConfig>();
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

        public async Task<int> WagesConfigCreateOrUpdateAsync(WagesConfig model)
        {
            try
            {
                SqlParameter[] param = {
                    new SqlParameter("@WagesConfigID", model.WagesConfigID)
                    ,new SqlParameter("@EmployeeCategoryId", model.EmployeeCategoryID)
                    ,new SqlParameter("@MinimumWages", model.MinimumWages)
                    ,new SqlParameter("@SpecialAllowance", model.SpecialAllowance)
                    ,new SqlParameter("@TotalWagesPerDay", model.TotalWagesPerDay)
                    ,new SqlParameter("@StartDate", model.StartDate)
                    ,new SqlParameter("@EndDate", model.EndDate)
                    ,new SqlParameter("@IsActive", model.IsActive)
                    ,new SqlParameter("@CreatedOrModifiedBy", model.CreatedOrModifiedBy)

                };

                var obj = await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_IU_WagesConfig", param);

                return obj != null ? Convert.ToInt32(obj) : 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
