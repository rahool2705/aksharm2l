using Business.Entities.EmployeeLeaveTxn;
using Business.Entities.LeaveTypeMaster;
using Business.SQL;
using System.Threading.Tasks;

namespace Business.Interface.ILeaveMaster
{
    public interface ILeaveMasterService
    {
        Task<int> AddUpdateEmployeeLeaveTxn(EmployeeLeaveTxn employeeLeaveTxn);
        Task<int> AddUpdateLeaveType(LeaveTypeMaster leaveTypeMaster);
        Task<int> CancelEmployeeLeave(EmployeeLeaveTxn employeeLeaveTxn);
        Task<PagedDataTable<EmployeeLeaveTxn>> GetAllEmployeeLeaveTxn(int pageNo = 1, int pageSize = 10, string searchString = "", string orderBy = "EmployeeID", string sortBy = "ASC");
        Task<PagedDataTable<LeaveTypeMaster>> GetAllLeaveTypeMaster(int pageNo = 1, int pageSize = 10, string searchString = "", string orderBy = "EmployeeID", string sortBy = "ASC");
        Task<EmployeeLeaveTxn> GetEmployeeLeaveTxnAsync(int employeeLeaveTxnID, int empliyeeId);
        Task<LeaveTypeMaster> GetLeaveTypeAsync(int leaveTypeId);
    }
}
