using Business.Entities;
using Business.Entities.Company;
using Business.SQL;
using System.Threading.Tasks;

namespace Business.Interface
{
    public interface ISiteCompanyRepository
    {
        #region "Add Compnay Slider"
        Task<CompanyMasterMetadata> GetCompanyAsync(int CompanyID);
        #endregion "Add Compnay Slider"

        #region "Compnay Master"
        Task<int> CreateOrUpdateCompanyAsync(CompanyMasterMetadata compnay);
        Task<CompanyMasterMetadata> GetCompnayAsync(string companyID);
        Task<PagedDataTable<CompanyMasterMetadata>> GetAllCompanyAsync(int pageNo, int pageSize, string searchString = "", string orderBy = "CompanyName", string sortBy = "ASC");
        Task<CompanyLogoMasterMetadata> GetCompanyLogoMaster(int companyID);
        #endregion 

        #region "Compnay contact"
        Task<PagedDataTable<CompanyContactTxnMetadata>> GetAllCompanyContactAsync(int companyID);
        Task<int> CreateOrUpdateCompanyContactAsync(CompanyContactTxnMetadata item);
        Task<CompanyContactTxnMetadata> GetCompnayContactAsync(int companyID, int companyContactPersonsID);
        #endregion

        #region "Compnay Address"
        Task<PagedDataTable<CompanyAddressTxnMetadata>> GetAllCompanyAddressAsync(int companyID);
        Task<int> CreateOrUpdateCompanyAddressAsync(CompanyAddressTxnMetadata item);
        Task<CompanyAddressTxnMetadata> GetCompnayAddressAsync(int companyID, int companyAddressTxnID);
        #endregion

        #region "Company Registration"       
               
        Task<int> CreateOrUpdateCompanyRegistrationAsync(CompanyMasterMetadata item);
        Task<CompanyRegistration> GetCompanyRegistration(int companyID, int CompanyRegistrationID);
        Task<int> AddUpdateCompanyRegistration(CompanyRegistration CompanyRegistration);

        #endregion

        #region "Company Banking"

        #region "SuperAdmin Bank List"
        Task<PagedDataTable<CompanyBankDetails>> GetAllCompanyBankingAsync(int companyID);

        Task<CompanyBankDetails> GetCompanyBankAccount(int CompanyBankDetailsId, int CompanyId);

        Task<int> AddUpdateCompanyBankDetails(CompanyBankDetails companyBankDetails);

        #endregion "SuperAdmin Bank List"

        Task<CompanyBankingMetadata> GetCompanyBankingAsync(int companyID, int companyBankingID);
        Task<int> CreateOrUpdateCompanyBankingAsync(CompanyBankingMetadata item);
        //Task<PagedDataTable<CompanyBankingMetadata>> GetAllCompanyBankingAsync(int companyID);



        #endregion

        #region "Company Document"

        #region "SuperAdmin Document"

        Task<PagedDataTable<CompanyDocument>> GetCompanyAllDocuments(int pageNo = 1, int pageSize = 5, string searchString = "", string orderBy = "", string sortBy = "ASC", int CompanyId = 0);

        #endregion"SuperAdmin Document"

        
        Task<CompanyDocument> GetCompanyDocument(int CompanyDocumentId, int CompanyId);
        Task<int> AddUpdateCompanyDocument(CompanyDocument CompanyDocument);

        Task<CompanyDocumentMetadata> GetDocumentAsync(int companyID, int companyDocumentsID);
        Task<int> CreateOrUpdateCompanyDocumentAsync(CompanyDocumentMetadata item);
        Task<PagedDataTable<CompanyDocumentMetadata>> GetAllCompanyDocumentAsync(int companyID);
        #endregion
    }
}
