using Business.Entities.Customer;
using Business.Entities.Employee;
using Business.SQL;
using System.Threading.Tasks;

namespace Business.Interface.ICustomer
{
    public interface ICustomerService
    {
        Task<int> ActiveInActiveCustomerDocument(CustomerDocument CustomerDocument);
        Task<int> AddUpdateCustomeAddress(CustomerAddressTxn customerAddressTxn);
        Task<int> AddUpdateCustomer(CustomerMaster customerMaster);
        Task<int> AddUpdateCustomerBankDetails(CustomerBankDetails customerBankDetails);
        Task<int> AddUpdateCustomerContactDetail(CustomerContactDetail customerContactDetail);
        Task<int> AddUpdateCustomerContactPerson(CustomerContactTxn customerContactTxn);
        Task<int> AddUpdateCustomerDocument(CustomerDocument CustomerDocument);
        Task<int> AddUpdateCustomerRegistration(CustomerRegistration customerRegistration);
        Task<int> AddUpdateCustomerSetting(CustomerSetting customerSetting);
        Task<PagedDataTable<CustomerMaster>> GetAllCustomerAsync(int pageNo = 1, int pageSize = 10, string searchString = "", string orderBy = "CustomerID", string sortBy = "ASC");
        Task<CustomerAddressTxn> GetCustomerAddressTxn(int customeAddressTxnId, int customerId);
        Task<PagedDataTable<CustomerAddressTxn>> GetCustomerAllAddressAsync(int pageNo = 1, int pageSize = 5, string searchString = "", string orderBy = "", string sortBy = "ASC", int customerId = 0);
        Task<PagedDataTable<CustomerBankDetails>> GetCustomerAllBankAccount(int pageNo = 1, int pageSize = 5, string searchString = "", string orderBy = "", string sortBy = "ASC", int customerId = 0);
        Task<PagedDataTable<CustomerContactTxn>> GetCustomerAllContactPerson(int pageNo = 1, int pageSize = 5, string searchString = "", string orderBy = "", string sortBy = "ASC", int customerId = 0);
        Task<CustomerMaster> GetCustomerAsync(int customerId);
        Task<CustomerBankDetails> GetCustomerBankAccount(int customerBankDetailsId, int customerId);
        Task<CustomerContactDetail> GetCustomerContactDetail(int customerId);
        Task<CustomerContactTxn> GetCustomerContactPerson(int customerContactID, int customerId);
        Task<CustomerDocument> GetCustomerDocument(int CustomerDocumentId, int CustomerId);
        Task<CustomerRegistration> GetCustomerRegistration(int customerId);
        Task<PagedDataTable<CustomerDocument>> GetCustomersAllDocuments(int pageNo = 1, int pageSize = 5, string searchString = "", string orderBy = "", string sortBy = "ASC", int CustomerId = 0);
        Task<CustomerSetting> GetCustomerSetting(int customerId);
        Task<int> UpdateCustomerLogoImage(CustomerLogoImage customerLogoImage);
    }
}
