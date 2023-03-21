using Business.Entities.Master.EmployementType;
using Business.Interface.IMaster.IEmployementType;
using Business.SQL;
using System;
using System.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Business.Entities.Master.EmployementType;
using Business.Entities.Master.EmployeeCategory;

namespace Business.Service.Master.EmployementTypeService
{
    public class EmployementTypeService : IEmployementTypeService
    {
        private IConfiguration _config { get; set; }
        private string connection = string.Empty;
        public EmployementTypeService(IConfiguration config)
        {
            _config = config;
            connection = _config.GetConnectionString("DefaultConnection");
        }
        public async Task<PagedDataTable<EmploymentType>> GetAllEmployementTypeAsync(int pageNo, int pageSize, string searchString, string orderBy, string sortBy)
        {
            DataTable table = new DataTable();
            int totalItemCount = 0;
            PagedDataTable<EmploymentType> lst = null;
            try
            {
                SqlParameter[] param = {
                        new SqlParameter("@PageNo",pageNo)
                        ,new SqlParameter("@PageSize",pageSize)
                        ,new SqlParameter("@SearchString",searchString)
                        ,new SqlParameter("@OrderBy",orderBy)
                        ,new SqlParameter("@SortBy",sortBy=="ASC"?0:1)

                        };
                using (DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_GetAll_EmploymentTypeMaster", param))
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
                    lst = table.ToPagedDataTableList<EmploymentType>(pageNo, pageSize, totalItemCount);
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
        public async Task<EmploymentType> GetEmployementTypeAsync(int EmploymentTypeID)
        {
            EmploymentType result = null;
            try
            {
                SqlParameter[] param = { new SqlParameter("@EmploymentTypeID", EmploymentTypeID) };
                DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_Get_EmploymentTypeMaster", param);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            result = dr.ToPagedDataTableList<EmploymentType>();
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
        public async Task<int> InsertOrUpdateEmployementTypeAsync(EmploymentType employementType)
        {
            try
            {
                SqlParameter[] param = {
                new SqlParameter("@EmploymentTypeID", employementType.EmploymentTypeID)
                ,new SqlParameter("@EmploymentTypeText", employementType.EmploymentTypeText)
                ,new SqlParameter("@IsActive", employementType.IsActive)
                ,new SqlParameter("@CreatedOrModifiedBy", employementType.CreatedOrModifiedBy)
                };

                var obj = await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_IU_EmploymentTypeMaster", param);

                return obj != null ? Convert.ToInt32(obj) : 0;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
