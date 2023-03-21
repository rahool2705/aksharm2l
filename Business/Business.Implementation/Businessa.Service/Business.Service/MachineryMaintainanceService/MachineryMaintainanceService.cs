using Business.Interface.IMachineryService;
using Business.SQL;
using System;
using System.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Business.Entities.PartyType;
using Business.Entities.Machinery.MachineryMaintainance;

namespace Business.Service.MachineryService
{
    public class MachineryMaintainanceService : IMachineryMaintainanceService
    {
        private IConfiguration _config { get; set; }
        private string connection = string.Empty;
        public MachineryMaintainanceService(IConfiguration config)
        {
            _config = config;
            connection = _config.GetConnectionString("DefaultConnection");
        }

        public async Task<PagedDataTable<MachineryMaintainance>> GetAllMachineryMaintainanceAsync(int pageNo, int pageSize, string searchString, string orderBy, string sortBy)
        {
            DataTable table = new DataTable();
            int totalItemCount = 0;
            PagedDataTable<MachineryMaintainance> lst = null;
            try
            {
                SqlParameter[] param = {
                        new SqlParameter("@PageNo",pageNo)
                        ,new SqlParameter("@PageSize",pageSize)
                        ,new SqlParameter("@SearchString",searchString)
                        ,new SqlParameter("@OrderBy",orderBy)
                        ,new SqlParameter("@SortBy",sortBy=="ASC"?0:1)

                        };
                using (DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_GetAll_MachineryMaintainance", param))
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
                    lst = table.ToPagedDataTableList<MachineryMaintainance>(pageNo, pageSize, totalItemCount);
                    return lst;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (table != null)
                    table.Dispose();
            }
        }

        public async Task<MachineryMaintainance> GetMachineryMaintainanceAsync(int MachineryMaintainanceID)
        {
            MachineryMaintainance result = null;
            try
            {
                SqlParameter[] param = { new SqlParameter("@MachineryMaintainanceID", MachineryMaintainanceID) };
                DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_Get_MachineryMaintainance", param);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            result = dr.ToPagedDataTableList<MachineryMaintainance>();
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

        public async Task<int> InsertOrUpdateMachineryMaintainanceAsync(MachineryMaintainance machineryMaintainance)
        {
            try
            {
                SqlParameter[] param = {
                new SqlParameter("@MachineryMaintainanceID", machineryMaintainance.MachineryMaintainanceID)
                ,new SqlParameter("@MachineryText", machineryMaintainance.MachineryText)
                ,new SqlParameter("@MaintainancePurpose", machineryMaintainance.MaintainancePurpose)
                ,new SqlParameter("@Note", machineryMaintainance.Note)
                ,new SqlParameter("@Date", machineryMaintainance.Date)                
                ,new SqlParameter("@Charges", machineryMaintainance.Charges)
                ,new SqlParameter("@IsActive", machineryMaintainance.IsActive)
                ,new SqlParameter("@CreatedOrModifiedBy", machineryMaintainance.CreatedOrModifiedBy)
                };

                var obj = await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_IU_MachineryMaintainance", param);

                return obj != null ? Convert.ToInt32(obj) : 0;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
