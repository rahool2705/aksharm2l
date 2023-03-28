using System.Data;
using System.Threading.Tasks;

namespace Business.Interface.IEmployeeAttendanceSummary
{
    public interface IEmployeeAttendanceSummaryService
    {
        public Task<DataSet> GetEmployeeAllAttendanceSummary(int employeeCategoryId = 0, int userId = 0, int month = 0, int year = 0, int departmentId = 0, string searchString = "");
        Task<DataSet> GetEmployeeAllDetailSummary(int employeecategoryId = 0, int departmentId = 0, string searchstring = null);
    }
}
