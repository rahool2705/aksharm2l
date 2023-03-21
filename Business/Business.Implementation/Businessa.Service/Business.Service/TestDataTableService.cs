using Business.Entities.Employee;
using Business.Interface;
using Business.SQL;
using System.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.Configuration;

namespace Business.Service
{
    public class TestDataTableService : ITestDataTable
    {
        private IConfiguration _config { get; set; }
        private string connection = string.Empty;
        public TestDataTableService(IConfiguration config)
        {
            _config = config;
            connection = _config.GetConnectionString("DefaultConnection");
        }
        public PagedDataTable<EmployeeMaster> GetAllEmployees()
        {
            DataTable table = new DataTable();
            int totalItemCount = 0;
            PagedDataTable<EmployeeMaster> lst = new PagedDataTable<EmployeeMaster>();
            try
            {
                SqlParameter[] param = {
                        new SqlParameter("@PageNo",1)
                        ,new SqlParameter("@PageSize","0")
                        ,new SqlParameter("@SearchString","")
                        ,new SqlParameter("@OrderBy","")
                        ,new SqlParameter("@SortBy","")
                        };
                using (DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "TestGetAll_EmployeeMaster", param))
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
                    lst = table.ToPagedDataTableList<EmployeeMaster>();
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
            return lst;
        }
    }
}
