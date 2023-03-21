using Business.Interface.IMaster.IEmployeeCategory;
using Business.SQL;
using System;
using System.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Business.Entities.Master.EmployeeCategory;
using Business.Entities.Machinery.MachineryMaintainance;

namespace Business.Service.Master.EmployeeCategoryService
{
    public class EmployeeCategoryService : IEmployeeCategoryService
    {
        private IConfiguration _config { get; set; }
        private string connection = string.Empty;
        public EmployeeCategoryService(IConfiguration config)
        {
            _config = config;
            connection = _config.GetConnectionString("DefaultConnection");
        }


        public async Task<PagedDataTable<EmployeeCategory>> GetAllEmployeeCategoryAsync(int pageNo, int pageSize, string searchString, string orderBy, string sortBy)
        {
            DataTable table = new DataTable();
            int totalItemCount = 0;
            PagedDataTable<EmployeeCategory> lst = null;
            try
            {
                SqlParameter[] param = {
                        new SqlParameter("@PageNo",pageNo)
                        ,new SqlParameter("@PageSize",pageSize)
                        ,new SqlParameter("@SearchString",searchString)
                        ,new SqlParameter("@OrderBy",orderBy)
                        ,new SqlParameter("@SortBy",sortBy=="ASC"?0:1)

                        };
                using (DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_GetAll_EmployeeCategoryMaster", param))
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
                    lst = table.ToPagedDataTableList<EmployeeCategory>(pageNo, pageSize, totalItemCount);
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

        public async Task<EmployeeCategory> GetEmployeeCategoryAsync(int EmployeeCategoryID)
        {
            EmployeeCategory result = null;
            try
            {
                SqlParameter[] param = { new SqlParameter("@EmployeeCategoryID", EmployeeCategoryID) };
                DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_Get_EmployeeCategoryMaster", param);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            result = dr.ToPagedDataTableList<EmployeeCategory>();
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

        public async Task<int> InsertOrUpdateEmployeeCategoryAsync(EmployeeCategory employeeCategory)
        {
            try
            {
                SqlParameter[] param = {
                new SqlParameter("@EmployeeCategoryId", employeeCategory.EmployeeCategoryID)
                ,new SqlParameter("@EmployeeCategoryText", employeeCategory.EmployeeCategoryText)
                ,new SqlParameter("@IsActive", employeeCategory.IsActive)
                ,new SqlParameter("@CreatedOrModifiedBy", employeeCategory.CreatedOrModifiedBy)
                };

                var obj = await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_IU_EmployeeCategoryMaster", param);

                return obj != null ? Convert.ToInt32(obj) : 0;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
