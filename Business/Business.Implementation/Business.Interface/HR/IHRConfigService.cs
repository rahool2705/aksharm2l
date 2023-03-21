using Business.Entities.HR;
using Business.SQL;
using System.Threading.Tasks;

namespace Business.Interface.HR
{
    public interface IHRConfigService
    {
        Task<PagedDataTable<HRConfig>> GetAllHRConfigAsync(int pageNo = 1, int pageSize = 20, string searchString = "", string orderBy = "EmployeeCategoryId", string sortBy = "ASC");
        Task<int> HRConfigCreateOrUpdateAsync(HRConfig model);

        Task<HRConfig> GetHRConfigAsync(int HRConfigID);
    }
}