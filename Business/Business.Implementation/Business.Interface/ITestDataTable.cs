using Business.Entities.Employee;
using Business.SQL;

namespace Business.Interface
{
    public interface ITestDataTable
    {
        PagedDataTable<EmployeeMaster> GetAllEmployees();
    }
}
