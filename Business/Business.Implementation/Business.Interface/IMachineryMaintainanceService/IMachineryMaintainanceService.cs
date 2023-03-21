using Business.Entities.Machinery.MachineryMaintainance;
using Business.SQL;
using System.Threading.Tasks;

namespace Business.Interface.IMachineryService
{
    public interface IMachineryMaintainanceService    {
        
        Task<PagedDataTable<MachineryMaintainance>> GetAllMachineryMaintainanceAsync(int pageNo, int pageSize, string searchString = "", string orderBy = "", string sortBy = "ASC");

        Task<MachineryMaintainance> GetMachineryMaintainanceAsync(int MachineryMaintainanceID);

        Task<int> InsertOrUpdateMachineryMaintainanceAsync(MachineryMaintainance machineryMaintainance);

        

    }
}
