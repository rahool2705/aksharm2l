using Business.SQL;
using System.Threading.Tasks;

namespace Business.Interface.CanteenCharges
{
    public interface ICanteenChargesService
    {
        Task<int> CanteenChargesCreateOrUpdateAsync(Entities.CanteenCharges.CanteenCharges model);
        Task<PagedDataTable<Entities.CanteenCharges.CanteenCharges>> GetAllCanteenChargesAsync(int pageNo = 1, int pageSize = 20, string searchString = "", string orderBy = "SrNo", string sortBy = "ASC");
        Task<Entities.CanteenCharges.CanteenCharges> GetCanteenChargesAsync(int canteenChargesID);
    }
}
