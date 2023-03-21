using Business.Entities.Machinery.MachineryMaintainance;
using Business.Entities.Master.EmployeeCategory;
using Business.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interface.IMaster.IEmployeeCategory
{
    public interface IEmployeeCategoryService
    {
        Task<PagedDataTable<EmployeeCategory>> GetAllEmployeeCategoryAsync(int pageNo, int pageSize, string searchString = "", string orderBy = "", string sortBy = "ASC");

        Task<EmployeeCategory> GetEmployeeCategoryAsync(int EmployeeCategoryID);

        Task<int> InsertOrUpdateEmployeeCategoryAsync(EmployeeCategory EmployeeCategory);

    }
}
