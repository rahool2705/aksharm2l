using Business.Entities.Master.EmployeeCategory;
using Business.Entities.Master.EmployementType;
using Business.SQL;
using System.Threading.Tasks;

namespace Business.Interface.IMaster.IEmployementType
{
    public interface IEmployementTypeService
    {     
        Task<PagedDataTable<EmploymentType>> GetAllEmployementTypeAsync(int pageNo, int pageSize, string searchString = "", string orderBy = "", string sortBy = "ASC");
        Task<EmploymentType> GetEmployementTypeAsync(int EmployementTypeID);

        Task<int> InsertOrUpdateEmployementTypeAsync(EmploymentType employementType);
    }
}
