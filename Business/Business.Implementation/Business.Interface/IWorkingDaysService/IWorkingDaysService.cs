using Business.Entities.WorkingDaysModel;
using Business.SQL;
using System.Threading.Tasks;

namespace Business.Interface.IWorkingDaysService
{
    public interface IWorkingDaysService
    {
        #region Index Page
        Task<PagedDataTable<WorkingDay>> GetAllWorkingDays(int pageNo, int pageSize, string searchString = "", string orderBy = "CompanyName", string sortBy = "ASC");

        #endregion Index Page

        #region Slider Page
        Task<WorkingDay> GetWorkingDays(int WorkCalendarID);
        #endregion Slider Page

        #region Add Or Update
        Task<int> AddOrUpdateWorkingDays(WorkingDay WorkingDay);
        #endregion Add Or Update
    }
}
