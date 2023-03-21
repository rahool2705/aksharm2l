using Business.Interface;
using Microsoft.AspNetCore.Http;
using Business.Interface.ICustomer;
using System.Collections.Generic;
using System.Linq;
using Business.Entities.Customer;
using Microsoft.AspNetCore.Mvc.Rendering;
using SelectList = Microsoft.AspNetCore.Mvc.Rendering.SelectList;
using Business.Entities.Employee;

namespace ERP.Extensions
{
    public class CustomerExtension
    {
        private static HttpContext Current => new HttpContextAccessor().HttpContext;
        public static IMasterService _masterService => (IMasterService)Current.RequestServices.GetService(typeof(IMasterService));
        public static ICustomerService _customerService => (ICustomerService)Current.RequestServices.GetService(typeof(ICustomerService));

        public static SelectList GetAllIndustryTypeMaster()
        {
            try
            {
                var listIndustryType = _masterService.GetAllIndustryTypeMaster();
                return new SelectList(listIndustryType, "IndustryTypeID", "IndustryTypeText");
            }
            catch
            {
                return new SelectList(Enumerable.Empty<SelectListItem>());
            }
        }
        public static SelectList GetAllBusinessTypeMaster()
        {
            try
            {
                var listBusinessType = _masterService.GetAllBusinessTypeMaster();
                return new SelectList(listBusinessType, "BusinessTypeID", "BusinessTypeText");
            }
            catch
            {
                return new SelectList(Enumerable.Empty<SelectListItem>());
            }
        }
        public static SelectList GetAllDepartments()
        {
            try
            {
                var listDepartment = _masterService.GetAllDepartments();
                return new SelectList(listDepartment, "DepartmentID", "DepartmentName");
            }
            catch
            {
                return new SelectList(Enumerable.Empty<SelectListItem>());
            }
        }
        public static SelectList GetAllDesignations()
        {
            try
            {
                var listDesignation = _masterService.GetAllDesignations();
                return new SelectList(listDesignation, "DesignationID", "DesignationText");
            }
            catch
            {
                return new SelectList(Enumerable.Empty<SelectListItem>());
            }
        }

        public static List<CustomerContactTxn> ListOfCustomerContactPerson(int customerId)
        {
            try
            {
                List<CustomerContactTxn> pds = _customerService.GetCustomerAllContactPerson(1, 10, "", "CustomerID", "1", customerId).Result;
                return pds;
            }
            catch
            {
                return null;
            }
        }

        public static List<CustomerAddressTxn> ListOfCustomerAddress(int customerId)
        {
            try
            {
                List<CustomerAddressTxn> pds = _customerService.GetCustomerAllAddressAsync(1, 10, "", "CustomerID", "1", customerId).Result;
                return pds;
            }
            catch
            {
                return null;
            }
        }

        public static CustomerContactDetail GetCustomerContactDetail(int customerId)
        {
            try
            {
                CustomerContactDetail customerContactDetail = new CustomerContactDetail();
                customerContactDetail.CustomerID = customerId;

                var contactDetail = _customerService.GetCustomerContactDetail(customerId).Result;

                if (contactDetail != null)
                    customerContactDetail = contactDetail;

                return customerContactDetail;
            }
            catch
            {
                return null;
            }
        }

        public static CustomerRegistration GetCustomerRegistration(int customerId)
        {
            try
            {
                CustomerRegistration customerRegistration = new CustomerRegistration();
                customerRegistration.CustomerID = customerId;

                var registration = _customerService.GetCustomerRegistration(customerId).Result;

                if (registration != null)
                    customerRegistration = registration;

                return customerRegistration;
            }
            catch
            {
                return null;
            }
        }

        public static List<CustomerBankDetails> GetCustomerAllBankAccount(int customerId)
        {
            try
            {
                List<CustomerBankDetails> pds = _customerService.GetCustomerAllBankAccount(1, 10, "", "BankName", "1", customerId).Result;
                return pds;
            }
            catch
            {
                return null;
            }
        }

        public static SelectList GetAllDocumentType()
        {
            try
            {
                var role = _masterService.GetAllDocumentTypeAsync().Result;
                return new SelectList(role, "DocumentTypeID", "DocumentTypeName");
            }
            catch
            {
                return new SelectList(Enumerable.Empty<SelectListItem>());
            }
        }
        public static List<CustomerDocument> GetCustomersAllDocuments(int CustomerId)
        {
            try
            {
                List<CustomerDocument> pds = _customerService.GetCustomersAllDocuments(1, 10, "", "CustomerID", "1", CustomerId).Result;
                return pds;
            }
            catch
            {
                return null;
            }
        }

        public static CustomerSetting GetCustomerSetting(int customerId)
        {
            try
            {
                CustomerSetting customerSetting = new CustomerSetting();
                customerSetting.CustomerID = customerId;

                var setting = _customerService.GetCustomerSetting(customerId).Result;

                if (setting != null)
                    customerSetting = setting;

                return customerSetting;
            }
            catch
            {
                return null;
            }
        }

        public static SelectList GetAllCustomerAsync()
        {
            try
            {
                var role = _customerService.GetAllCustomerAsync().Result;
                return new SelectList(role, "CustomerID", "CustomerName");
            }
            catch
            {
                return new SelectList(Enumerable.Empty<SelectListItem>());
            }
        }
    }
}
