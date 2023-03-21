using Business.Entities.Contractor;
using Business.SQL;
using System.Threading.Tasks;

namespace Business.Interface.IContractor
{
    public interface IContractorService
    {
        #region Contrator Index Page List
        Task<PagedDataTable<ContractorMaster>> GetAllContractorAsync(int pageNo = 1, int pageSize = 10, string searchString = "", string orderBy = "ContractorID", string sortBy = "ASC");
        #endregion Contrator Index Page List

        #region Contractor Basic Details
        Task<ContractorMaster> GetContractorAsync(int contractorID);
        Task<int> AddUpdateContractor(ContractorMaster contractorMaster);
        Task<int> UpdateContractorLogoImage(LeadLogoImage contractorLogoImage);
        #endregion Contractor Basic Details

        #region Contractor Contact Details

        Task<PagedDataTable<ContractorContactTxn>> GetContractorAllContactPerson(int pageNo = 1, int pageSize = 5, string searchString = "", string orderBy = "", string sortBy = "ASC", int contractorId = 0);

        Task<ContractorContactTxn> GetContractorContactPerson(int contratorContactID, int contratorId);

        Task<int> AddUpdateContractorContactPerson(ContractorContactTxn contractorContactTxn);

        #endregion Contractor Contact Details

        #region Contractor Address 

        Task<PagedDataTable<LeadAddressTxn>> GetContractorAllAddressAsync(int pageNo = 1, int pageSize = 5, string searchString = "", string orderBy = "", string sortBy = "ASC", int ContractorId = 0);

        Task<LeadAddressTxn> GetContractorAddressTxn(int customeAddressTxnId, int ContractorId);
        Task<int> AddUpdateContractorAddress(LeadAddressTxn contractorAddressTxn);

        #endregion Contractor Address

        #region Contractor Contact Detail

        Task<LeadContactDetail> GetContractorContactDetail(int ContractorId);

        Task<int> AddUpdateContractorContactDetail(LeadContactDetail contractorContactDetail);

        #endregion Contractor Contact Detail

        #region Contractor Registration
        Task<LeadRegistration> GetContractorRegistration(int ContractorId);

        Task<int> AddUpdateContractorRegistration(LeadRegistration contractorRegistration);

        #endregion Contractor Registration 

        #region Contractor Bank Details

        Task<PagedDataTable<LeadBankDetails>> GetContractorAllBankAccount(int pageNo = 1, int pageSize = 5, string searchString = "", string orderBy = "", string sortBy = "ASC", int ContractorId = 0);

        Task<LeadBankDetails> GetContractorBankAccount(int ContractorBankDetailsId, int ContractorId);

        Task<int> AddUpdateContractorBankDetails(LeadBankDetails contractorBankDetails);

        #endregion Contractor Bank Details

        #region Contractor Setting

        Task<LeadSetting> GetContractorSetting(int ContractorId);
        Task<int> AddUpdateContractorSetting(LeadSetting ContractorSetting);

        #endregion Contractor Setting


        #region Contractor Document
        Task<PagedDataTable<LeadDocument>> GetContractorsAllDocuments(int pageNo = 1, int pageSize = 5, string searchString = "", string orderBy = "", string sortBy = "ASC", int ContractorId = 0);
        Task<LeadDocument> GetContractorDocument(int ContractorDocumentId, int ContractorId);
        Task<int> AddUpdateContractorDocument(LeadDocument contractorDocument);

        #endregion Contractor Document
    }
}
