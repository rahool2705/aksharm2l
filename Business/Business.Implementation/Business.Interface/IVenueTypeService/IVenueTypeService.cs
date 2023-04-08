using Business.Entities.VenueTypeModel;
using Business.SQL;
using System.Threading.Tasks;

namespace Business.Interface.IVenueTypeService
{
    public interface IVenueTypeService
    {
        #region Index Page
        Task<PagedDataTable<VenueType>> GetAllVenueType(int pageNo, int pageSize, string searchString = "", string orderBy = "VenueName", string sortBy = "ASC");
        #endregion Index Page

        #region Slider Calling
        Task<VenueType> GetVenueType(int VenueTypeID);
        #endregion Slider Calling

        #region Add Or Update
        Task<int> VenueTypeAddOrUpdate(VenueType model);
        #endregion Add Or Update
    }
}
