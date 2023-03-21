using System;
using System.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;
using Business.Interface.HR;
using Business.SQL;
using Microsoft.Extensions.Configuration;
using Business.Entities.HR;

namespace Business.Service.HR
{
    public class HRConfigService : IHRConfigService
    {
        private IConfiguration _config { get; set; }
        private string connection = string.Empty;
        public HRConfigService(IConfiguration config)
        {
            _config = config;
            connection = _config.GetConnectionString("DefaultConnection");
        }
        public async Task<PagedDataTable<HRConfig>> GetAllHRConfigAsync(int pageNo = 1, int pageSize = 20, string searchString = "", string orderBy = "EmployeeCategoryId", string sortBy = "ASC")
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

                using (DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_GetAll_HRConfig", param))
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
                    PagedDataTable<HRConfig> lst = table.ToPagedDataTableList<HRConfig>
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

        public async Task<HRConfig> GetHRConfigAsync(int HRConfigID)
        {
            HRConfig result = null;
            try
            {
                SqlParameter[] param = { new SqlParameter("@HRConfigID", HRConfigID) };
                DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_Get_HRConfig", param);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            result = dr.ToPagedDataTableList<HRConfig>();
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

        public async Task<int> HRConfigCreateOrUpdateAsync(HRConfig model)
        {
            try
            {
                SqlParameter[] param = {
                    new SqlParameter("@HRConfigID", model.HRConfigID),
                    new SqlParameter("@EmployeeCategoryId", model.EmployeeCategoryId),                    
                    new SqlParameter("@Year", model.Year),
                    new SqlParameter("@WorkingDayInMonth", model.WorkingDayInMonth),
                    new SqlParameter("@WorkingHrsInDay", model.WorkingHrsInDay),
                    new SqlParameter("@OTPaymentIn", model.OTPaymentIn),
                    new SqlParameter("@WeekOff1", model.WeekOff1),
                    new SqlParameter("@WeekOff2", model.WeekOff2),
                    new SqlParameter("@IsActive", true),
                    new SqlParameter("@CreatedOrModifiedBy", model.CreatedOrModifiedBy),
                    new SqlParameter("@InterestOnLoan",model.InterestOnLoan)
                };

                var obj = await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_IU_HRConfig", param);

                return obj != null ? Convert.ToInt32(obj) : 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
