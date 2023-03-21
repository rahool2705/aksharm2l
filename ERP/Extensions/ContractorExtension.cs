using Business.Entities.Contractor;
using Business.Entities.Contractor;
using Business.Interface.IContractor;
using Business.Interface;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Business.Interface.IContractor;
using System.Linq;
using System.Web.Mvc;
using SelectList = Microsoft.AspNetCore.Mvc.Rendering.SelectList;
using Business.Service.Contractor;
using Business.Entities.Contractor;
using Business.Entities.Contractor;
using Business.Entities.Contractor;

namespace ERP.Extensions
{
    public class ContractorExtension
    {
        private static HttpContext Current => new HttpContextAccessor().HttpContext;
        public static IMasterService _masterService => (IMasterService)Current.RequestServices.GetService(typeof(IMasterService));
        public static IContractorService _contractorService => (IContractorService)Current.RequestServices.GetService(typeof(IContractorService));

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

        public static List<ContractorContactTxn> ListOfContractorContactPerson(int contractorId)
        {
            try
            {
                List<ContractorContactTxn> pds = _contractorService.GetContractorAllContactPerson(1, 10, "", "ContractorID", "1", contractorId).Result;
                return pds;
            }
            catch
            {
                return null;
            }
        }

        public static List<LeadAddressTxn> ListOfContractorAddress(int contractorId)
        {
            try
            {
                List<LeadAddressTxn> pds = _contractorService.GetContractorAllAddressAsync(1, 10, "", "ContractorID", "1", contractorId).Result;
                return pds;
            }
            catch
            {
                return null;
            }
        }

        public static LeadContactDetail GetContractorContactDetail(int contractorId)
        {
            try
            {
                LeadContactDetail contractorContactDetail = new LeadContactDetail();
                contractorContactDetail.ContractorID = contractorId;

                var contactDetail = _contractorService.GetContractorContactDetail(contractorId).Result;

                if (contactDetail != null)
                    contractorContactDetail = contactDetail;

                return contractorContactDetail;
            }
            catch
            {
                return null;
            }
        }


        public static LeadRegistration GetContractorRegistration(int contractorId)
        {
            try
            {
                LeadRegistration contractorRegistration = new LeadRegistration();
                contractorRegistration.ContractorID = contractorId;

                var registration = _contractorService.GetContractorRegistration(contractorId).Result;

                if (registration != null)
                    contractorRegistration = registration;

                return contractorRegistration;
            }
            catch
            {
                return null;
            }
        }

        public static List<LeadBankDetails> GetContractorAllBankAccount(int ContractorId)
        {
            try
            {
                List<LeadBankDetails> pds = _contractorService.GetContractorAllBankAccount(1, 10, "", "BankName", "1", ContractorId).Result;
                return pds;
            }
            catch
            {
                return null;
            }
        }

        public static LeadSetting GetContractorSetting(int ContractorId)
        {
            try
            {
                LeadSetting ContractorSetting = new LeadSetting();
                ContractorSetting.ContractorID = ContractorId;

                var setting = _contractorService.GetContractorSetting(ContractorId).Result;

                if (setting != null)
                    ContractorSetting = setting;

                return ContractorSetting;
            }
            catch
            {
                return null;
            }
        }

        public static List<LeadDocument> GetContractorsAllDocuments(int ContractorId)
        {
            try
            {
                List<LeadDocument> pds = _contractorService.GetContractorsAllDocuments(1, 10, "", "ContractorID", "1", ContractorId).Result;
                return pds;
            }
            catch
            {
                return null;
            }
        }

    }
}
