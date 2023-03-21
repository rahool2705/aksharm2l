using Business.Entities.EmployeeAdvances;
using Business.Entities.EmployeeLoan;
using Business.SQL;
using System.Threading.Tasks;

namespace Business.Interface.ILoanAdvanceService
{
    public interface ILoanAdvanceService
    {
        Task<int> EmployeeAdvancesCreateOrUpdateAsync(EmployeeAdvances model);
        Task<int> EmployeeLoanCreateOrUpdateAsync(EmployeeLoan model);
        Task<PagedDataTable<EmployeeAdvances>> GetAllEmployeeAdvancesAsync(int pageNo = 1, int pageSize = 20, string searchString = "", string orderBy = "SrNo", string sortBy = "ASC");
        Task<PagedDataTable<EmployeeLoan>> GetAllEmployeeLoanAsync(int pageNo = 1, int pageSize = 20, string searchString = "", string orderBy = "SrNo", string sortBy = "ASC");
        Task<EmployeeAdvances> GetEmployeeAdvancesAsync(int employeeAdvancesID);
        Task<EmployeeLoan> GetEmployeeLoanAsync(int employeeLoanID);
    }
}
