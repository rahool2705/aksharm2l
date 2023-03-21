using Business.Entities.Machinery.MachineryMaster;
using Business.SQL;
using System.Threading.Tasks;

namespace Business.Interface.IMachineryMasterService
{
    public interface IMachineryMasterService
    {
        Task<PagedDataTable<MachineryMaster>> GetAllMachineryMasterAsync(int pageNo, int pageSize, string searchString = "", string orderBy = "", string sortBy = "ASC");
    }
}
