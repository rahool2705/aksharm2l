using Business.Interface.IWorkingDaysService;
using Business.SQL;
using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;
using Business.Entities.WorkingDaysModel;

namespace Business.Service.SWorkingDays
{
    public class WorkingDaysService : IWorkingDaysService
    {
        #region Database Connection
        private IConfiguration _config { get; set; }
        private string connection = string.Empty;
        public WorkingDaysService(IConfiguration config)
        {
            _config = config;
            connection = _config.GetConnectionString("DefaultConnection");
        }
        #endregion Database Connection

        #region Index Page

        public async Task<PagedDataTable<WorkingDay>> GetAllWorkingDays(int pageNo, int pageSize, string searchString = "", string orderBy = "CompanyName", string sortBy = "ASC")
        {
            DataTable table = new DataTable();
            int totalItemCount = 0;
            PagedDataTable<WorkingDay> lst = null;
            try
            {
                SqlParameter[] param = {
                        new SqlParameter("@PageNo",pageNo)
                        ,new SqlParameter("@PageSize",pageSize)
                        ,new SqlParameter("@SearchString",searchString)
                        ,new SqlParameter("@OrderBy",orderBy)
                        ,new SqlParameter("@SortBy",sortBy=="ASC"?0:1)

                        };
                using (DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_GetAll_WorkCalendar", param))
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
                    lst = table.ToPagedDataTableList<WorkingDay>(pageNo, pageSize, totalItemCount);
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

        #endregion Index Page

        #region Slider Page

        public async Task<WorkingDay> GetWorkingDays(int WorkCalendarID)
        {
            WorkingDay result = null;
            try
            {
                SqlParameter[] param = { new SqlParameter("@WorkCalendarID", WorkCalendarID) };
                DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_Get_WorkCalendar", param);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            result = dr.ToPagedDataTableList<WorkingDay>();
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

        #endregion Slider Page

        #region Add Or Update

        public async Task<int> AddOrUpdateWorkingDays(WorkingDay model)
        {
            try
            {
                SqlParameter[] param = {
                new SqlParameter("@WorkCalendarID", model.WorkCalendarID)
                ,new SqlParameter("@Year", model.Year)
                ,new SqlParameter("@Month", model.Month)
                ,new SqlParameter("@WorkingDays", model.WorkingDays)
                ,new SqlParameter("@Holidays", model.Holidays)
                ,new SqlParameter("@IsActive", model.IsActive)
                ,new SqlParameter("@CreatedOrModifiedBy", model.CreatedOrModifiedBy)
                };

                var obj = await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_IU_WorkCalendar", param);

                return obj != null ? Convert.ToInt32(obj) : 0;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion Add Or Update


    }
}
