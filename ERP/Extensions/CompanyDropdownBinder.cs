using Business.Entities;
using Business.Entities.Company;
using Business.Interface;
using Business.Interface.Dynamic;
using Business.SQL;
using ERP.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
namespace ERP.Extensions
{
    public static class CompanyDropdownBinder
    {
        private static HttpContext Current => new HttpContextAccessor().HttpContext;
        private static ISiteRoleRepository roleService => (ISiteRoleRepository)Current.RequestServices.GetService(typeof(ISiteRoleRepository));
        private static ISuperAdminService superAdmin => (ISuperAdminService)Current.RequestServices.GetService(typeof(ISuperAdminService));
        private static ISiteCompanyRepository compnayService => (ISiteCompanyRepository)Current.RequestServices.GetService(typeof(ISiteCompanyRepository));
        private static IEntity _entity => (IEntity)Current.RequestServices.GetService(typeof(IEntity));
        private static IRequestType _requestType => (IRequestType)Current.RequestServices.GetService(typeof(IRequestType));
        private static IMasterEntity masterEntity => (IMasterEntity)Current.RequestServices.GetService(typeof(IMasterEntity));

        #region "Contact list"
        public static PagedDataTable<CompanyContactTxnMetadata> ListOfCompnayContact(int companyID)
        {
            try
            {
                PagedDataTable<CompanyContactTxnMetadata> pds = compnayService.GetAllCompanyContactAsync(companyID).Result;
                return pds;
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region "Address list"
        public static PagedDataTable<CompanyAddressTxnMetadata> ListOfCompnayAddress(int companyID)
        {
            try
            {
                PagedDataTable<CompanyAddressTxnMetadata> pds = compnayService.GetAllCompanyAddressAsync(companyID).Result;
                return pds;
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region "Bank list"

        #region "SuperAdmin Bank List"
        public static PagedDataTable<CompanyBankDetails> ListOfCompnayBank(int CompanyID)
        {
            try
            {
                PagedDataTable<CompanyBankDetails> pds = compnayService.GetAllCompanyBankingAsync(CompanyID).Result;
                return pds;
            }
            catch
            {
                return null;
            }
        }
        #endregion "SuperAdmin Bank List"

        /*public static PagedDataTable<CompanyBankingMetadata> ListOfCompnayBank(int companyID)
        {
            try
            {
                PagedDataTable<CompanyBankingMetadata> pds = compnayService.GetAllCompanyBankingAsync(companyID).Result;
                return pds;
            }
            catch
            {
                return null;
            }
        }*/
        #endregion

        #region "Document list"

        #region "SuperAdmin Document"
        public static List<CompanyDocument> GetCompanyAllDocuments(int CompanyId)
        {
            try
            {
                List<CompanyDocument> pds = compnayService.GetCompanyAllDocuments(1, 10, "", "CompanyID", "1", CompanyId).Result;
                return pds;
            }
            catch
            {
                return null;
            }
        }
        #endregion "SuperAdmin Document"

        public static PagedDataTable<CompanyDocumentMetadata> ListOfCompnayDocuments(int companyID)
        {
            try
            {
                PagedDataTable<CompanyDocumentMetadata> pds = compnayService.GetAllCompanyDocumentAsync(companyID).Result;
                return pds;
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region "Registration"

        public static CompanyRegistration GetCompanyRegistration(int companyID, int companyRegistrationID)
        {
            try
            {
                CompanyRegistration companyRegistration = new CompanyRegistration();
                var companyregistration = compnayService.GetCompanyRegistration(companyID, companyRegistrationID).Result;
                if (companyregistration == null)
                    companyRegistration.CompanyID = companyID;
                else
                    companyRegistration = companyregistration;
                return companyRegistration;
            }
            catch
            {
                return null;
            }
        }

        #endregion "Registration"

        #region "ContactDetail"

        #endregion "Contact Detail"


        public static SelectList EntityType(int CompanyID)
        {
            try
            {
                var pds = _entity.GetList(CompanyID).Where(a => a.IsActive).ToList();
                return new SelectList(pds, "MasterListID", "Value");
            }
            catch
            {
                return new SelectList(Enumerable.Empty<SelectListItem>());
            }
        }
        public static SelectList RequestSection(int CompanyID)
        {
            try
            {
                var pds = _requestType.GetList(CompanyID).Where(p => p.IsActive).ToList();
                return new SelectList(pds, "RequestTypeID", "Name");
            }
            catch
            {
                return new SelectList(Enumerable.Empty<SelectListItem>());
            }
        }
        public static SelectList MasterDataKey(int CompanyID)
        {
            try
            {
                var pds = masterEntity.GetDropdownKeys(CompanyID);
                return new SelectList(pds, "Value", "Text");
            }
            catch
            {
                return new SelectList(Enumerable.Empty<SelectListItem>());
            }
        }

    }
}
