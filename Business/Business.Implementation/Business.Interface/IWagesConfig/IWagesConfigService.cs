using Business.Entities.WagesConfig;
using Business.SQL;
using System.Threading.Tasks;

namespace Business.Interface.IWagesConfig
{
    public interface IWagesConfigService
    {
        Task<PagedDataTable<WagesConfig>> GetAllWagesConfigAsync(int pageNo = 1, int pageSize = 20, string searchString = "", string orderBy = "SrNo", string sortBy = "ASC");
        Task<WagesConfig> GetWagesConfigAsync(int WagesConfigID);
        Task<int> WagesConfigCreateOrUpdateAsync(WagesConfig model);
    }
}
