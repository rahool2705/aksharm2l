using Business.Entities.PositiveNoteModel;
using Business.SQL;
using System.Threading.Tasks;

namespace Business.Interface.IPositiveNoteService
{
    public interface IPositiveNoteService
    {        
        #region Index page
        Task<PagedDataTable<PositiveNote>> GetAllPositiveNoteAsync(int pageNo, int pageSize, string searchString = "", string orderBy = "PositiveNoteText", string sortBy = "ASC");
        #endregion Index page

        #region Get Single Record
        Task<PositiveNote> GetPositiveNoteAsync(int PositiveNoteID);
        #endregion Get Single Record

        #region Add or Update Positive Note
        Task<int> AddOrUpdatePositiveNote(PositiveNote model);
        #endregion Add or Update Positive Note

    }
}
