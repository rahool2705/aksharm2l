using Business.Entities.SalaryFormula;
using Business.SQL;
using System.Threading.Tasks;

namespace Business.Interface.ISalaryFormula
{
    public interface ISalaryFormula
    {
        Task<int> AddUpdateSalaryCalculationFormula(SalaryFormulaMaster salaryFormulaMaster);
        Task<int> AddUpdateSalaryFormula(SalaryFormula salaryFormula);
        Task<int> AddUpdateSalaryHead(SalaryHead salaryHead);
        Task<PagedDataTable<SalaryFormula>> GetAllSalaryFormula(int employeeCategoryId, int pageNo = 1, int pageSize = 10, string searchString = "", string orderBy = "SrNo", string sortBy = "ASC");
        Task<PagedDataTable<SalaryHead>> GetAllSalaryHeads(int pageNo = 1, int pageSize = 10, string searchString = "", string orderBy = "SrNo", string sortBy = "ASC");
        Task<SalaryFormulaMaster> GetSalaryFormula();
        Task<SalaryFormula> GetSalaryFormulaAsync(int salaryFormulaId);
        Task<SalaryHead> GetSalaryHeadAsync(int salaryHeadId);
    }
}
