using Business.Entities.Employee;
using Business.Entities.Employee.EmployeeMedicalHistory;
using Business.Entities.Employee.EmployeeMedicalInsurance;
using Business.Entities.SalaryPaidHr;
using Business.SQL;
using System.Data;
using System.Threading.Tasks;

namespace Business.Interface.IEmployee
{
    public interface IEmployeeService
    {
        //Task<PagedDataTable<EmployeeMaster>> GetAllEmployeesAsync(int pageNo, int pageSize, string searchString = "", string orderBy = "EmployeeID", string sortBy = "ASC");
        Task<PagedDataTable<EmployeeMaster>> GetAllEmployeesAsync(int pageNo = 1, int pageSize = 10, string searchString = "", string orderBy = "EmployeeID", string sortBy = "ASC");
        #region Basic detail
        Task<AddUpdateEmployee> GetEmployeeAsync(int employeeId);
        Task<int> AddUpdateEmployee(AddUpdateEmployee addUpdateEmployee);
        Task<int> UpdateEmployeeProfilePhoto(EmployeeProfileImage employeeProfileImage);
        #endregion Basic detail

        #region Address detail

        Task<PagedDataTable<EmployeeAddressTxn>> GetEmployeesAllAddressAsync(int pageNo = 1, int pageSize = 10, string searchString = "", string orderBy = "EmployeeID", string sortBy = "ASC", int employeeId = 0);
        Task<int> CreateOrUpdateEmployeeAddressAsync(EmployeeAddressTxn addressMaster);
        Task<EmployeeAddressTxn> GetEmployeeAddressTxn(int employeeAddressTxnId, int employeeId);

        #endregion Address detail

        #region Family Background detail
        Task<int> CreateOrUpdateEmployeeFamilyBackgroundAsync(EmployeeFamilyDetail employeeFamilyDetail);
        Task<EmployeeFamilyDetail> GetEmployeeFamily(int employeeId);

        #endregion Family Background detail

        #region Employee banking detail
        Task<PagedDataTable<EmployeeBankDetails>> GetEmployeesAllBankAccount(int pageNo = 1, int pageSize = 5, string searchString = "", string orderBy = "", string sortBy = "ASC", int employeeId = 0);
        Task<int> CreateOrUpdateEmployeeBankDetail(EmployeeBankDetails employeeBankDetails);
        Task<EmployeeBankDetails> GetEmployeeBankAccount(int employeeBankDetailId, int employeeId);
        #endregion Employee banking detail

        #region Employee document
        Task<PagedDataTable<EmployeeDocument>> GetEmployeesAllDocuments(int pageNo = 1, int pageSize = 5, string searchString = "", string orderBy = "", string sortBy = "ASC", int employeeId = 0);
        Task<EmployeeDocument> GetEmployeeDocument(int employeeDocumentId, int employeeId);
        Task<int> CreateOrUpdateEmployeeDocument(EmployeeDocument employeeDocument);
        Task<int> ActiveInActiveEmployeeDocument(EmployeeDocument employeeDocument);
        #endregion Employee document

        #region Employee Education

        Task<PagedDataTable<EmployeeEducation>> GetEmployeesAllEducation(int pageNo = 1, int pageSize = 5, string searchString = "", string orderBy = "", string sortBy = "ASC", int employeeId = 0);
        Task<EmployeeEducation> AddUpdateEmployeeEducation(int employeeEducationId, int employeeId);
        Task<int> AddUpdateEmployeeEducation(EmployeeEducation employeeEducation);
        Task<PagedDataTable<EmployeeExperience>> GetEmployeesAllExperience(int pageNo = 1, int pageSize = 10, string searchString = "", string orderBy = "", string sortBy = "ASC", int employeeId = 0);
        Task<EmployeeExperience> AddUpdateEmployeeExperience(int employeeExperienceId, int employeeId);
        Task<int> AddUpdateEmployeeExperience(EmployeeExperience employeeExperience);
        PagedDataTable<EmploymentStatusMaster> GetAllEmploymentStatus();
        Task<EmployeeHRAdministration> GetEmployeeHRAdministration(int employeeId);
        Task<int> AddUpdateEmployeeHRAdministration(EmployeeHRAdministration employeeHRAdministration);
        Task<EmployeeSalaryBreakup> GetEmployeeSalaryBreakup(int employeeId);
        Task<int> AddUpdateEmployeeSalaryBreakup(EmployeeSalaryBreakup employeeSalaryBreakup);
        Task<int> AddUpdateEmployeePresent(EmployeePresent employeePresent, DataTable dataTable);

        #endregion Employee Education

        #region Employee Identity

        Task<PagedDataTable<EmployeeIdentity>> GetEmployeeAllIdentity(int pageNo = 1, int pageSize = 10, string searchString = "", string orderBy = "", string sortBy = "ASC", int employeeId = 0);
        Task<EmployeeIdentity> AddUpdateEmployeeIdentity(int employeeIdentityId, int employeeId);

        Task<int> AddUpdateEmployeeIdentity(EmployeeIdentity employeeIdentity);

        #endregion Employee Identity

        #region Employee Medical History

        Task<PagedDataTable<EmployeeMedicalHistory>> GetEmployeeAllMedicalHistory(int pageNo = 1, int pageSize = 10, string searchString = "", string orderBy = "", string sortBy = "ASC", int employeeId = 0);

        Task<EmployeeMedicalHistory> AddUpdateEmployeeMedicalHistory(int employeeMedicalDetailsID, int employeeId);

        Task<int> AddUpdateEmployeeMedicalHistory(EmployeeMedicalHistory employeeMedicalHistory);

        #endregion Employee Medical History


        #region Employee Medical Insurance

        Task<PagedDataTable<EmployeeMedicalInsurance>> GetEmployeeAllMedicalInsurance(int pageNo = 1, int pageSize = 10, string searchString = "", string orderBy = "", string sortBy = "ASC", int employeeId = 0);

        Task<EmployeeMedicalInsurance> AddUpdateEmployeeMedicalInsurance(int employeeMedicalInsuranceId, int employeeId);

       Task<int> AddUpdateEmployeeMedicalInsurance(EmployeeMedicalInsurance employeeMedicalInsurance);

        #endregion Employee Medical Insurance

        #region Employee Salary Paid Hours

        Task<int> AddUpdateSalaryPaidHr(SalaryPaidHr salaryPaidHr);
        Task<SalaryPaidHr> GetSalaryPaidHr(int employeeId);

        #endregion Employee Salary Paid Hours

    }
}