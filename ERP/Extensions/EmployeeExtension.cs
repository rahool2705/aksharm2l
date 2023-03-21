using Business.Entities.Employee;
using Business.Entities.Employee.EmployeeMedicalHistory;
using Business.Entities.Employee.EmployeeMedicalInsurance;
using Business.Interface;
using Business.Interface.IEmployee;
using Business.Interface.IMaster.IEmployeeCategory;
using Business.Service.Dynamic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace ERP.Extensions
{
    public class EmployeeExtension
    {
        private static HttpContext Current => new HttpContextAccessor().HttpContext;
        public static IEmployeeService _employeeService => (IEmployeeService)Current.RequestServices.GetService(typeof(IEmployeeService));
        public static IMasterService _masterService => (IMasterService)Current.RequestServices.GetService(typeof(IMasterService));
        public static IEmployeeCategoryService _employeeCategoryService => (IEmployeeCategoryService)Current.RequestServices.GetService(typeof(IEmployeeCategoryService));
        public static ISiteCompanyRepository _siteCompanyRepository => (ISiteCompanyRepository)Current.RequestServices.GetService(typeof(ISiteCompanyRepository));
        #region "AddressList"
        public static List<EmployeeAddressTxn> ListOfEmployeeAddress(int employeeId)
        {
            try
            {
                List<EmployeeAddressTxn> pds = _employeeService.GetEmployeesAllAddressAsync(1, 10, "", "EmployeeID", "1", employeeId).Result;
                return pds;
            }
            catch
            {
                return null;
            }
        }

        #endregion "AddressList"

        #region Employee Family Detail
        public static EmployeeFamilyDetail GetEmployeeFamilyDetail(int employeeId)
        {
            try
            {
                EmployeeFamilyDetail employeeFamily = new EmployeeFamilyDetail();
                employeeFamily.EmployeeID = employeeId;

                var employeeFamilyDetail = _employeeService.GetEmployeeFamily(employeeId).Result;

                if (employeeFamilyDetail != null)
                    employeeFamily = employeeFamilyDetail;

                return employeeFamily;
            }
            catch
            {
                return null;
            }
        }
        #endregion Employee Family Detail

        #region Employee banking detail
        public static List<EmployeeBankDetails> GetEmployeesAllBankAccount(int employeeId)
        {
            try
            {
                List<EmployeeBankDetails> pds = _employeeService.GetEmployeesAllBankAccount(1, 10, "", "EmployeeID", "1", employeeId).Result;
                return pds;
            }
            catch
            {
                return null;
            }
        }
        #endregion Employee banking detail

        #region Employee document
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
        public static List<EmployeeDocument> GetEmployeesAllDocuments(int employeeId)
        {
            try
            {
                List<EmployeeDocument> pds = _employeeService.GetEmployeesAllDocuments(1, 10, "", "EmployeeID", "1", employeeId).Result;
                return pds;
            }
            catch
            {
                return null;
            }
        }
        #endregion Employee document

        public static SelectList GetMaritalStatusMaster()
        {
            try
            {
                var role = _masterService.GetMaritalStatusMaster();
                return new SelectList(role, "MaritalStatusID", "MaritalStatusText");
            }
            catch
            {
                return new SelectList(Enumerable.Empty<SelectListItem>());
            }
        }

        public static SelectList GetAllBloodGroupMaster()
        {
            try
            {
                var bloodGroupMaster = _masterService.GetAllBloodGroupMaster().Result;
                return new SelectList(bloodGroupMaster, "BloodGroupID", "BloodGroupText");
            }
            catch
            {
                return new SelectList(Enumerable.Empty<SelectListItem>());
            }
        }

        #region Employee Education

        public static List<EmployeeEducation> GetEmployeesAllEducation(int employeeId)
        {
            try
            {
                List<EmployeeEducation> pds = _employeeService.GetEmployeesAllEducation(1, 10, "", "Degree", "1", employeeId).Result;
                return pds;
            }
            catch
            {
                return null;
            }
        }

        #endregion Employee Education

        #region Employee Experience

        /*public static SelectList GetAllEmploymentTypeMaster()
        {
            try
            {
                var idProofTypes = _masterService.GetIdentityProofTypeAsync();
                return new SelectList(idProofTypes, "IdentityProofTypeID", "IdentityProofTypeText");
            }
            catch
            {
                return new SelectList(Enumerable.Empty<SelectListItem>());
            }
        }*/
        public static SelectList GetAllEmploymentTypeMaster()
        {
            try
            {
                var role = _masterService.GetAllEmploymentTypeMaster();
                return new SelectList(role, "EmploymentTypeID", "EmploymentTypeText");
            }
            catch
            {
                return new SelectList(Enumerable.Empty<SelectListItem>());
            }
        }

        public static SelectList GetAllEmploymentLocationTypeMaster()
        {
            try
            {
                var role = _masterService.GetAllEmploymentLocationTypeMaster();
                return new SelectList(role, "EmploymentLocationTypeID", "EmploymentLocationTypeText");
            }
            catch
            {
                return new SelectList(Enumerable.Empty<SelectListItem>());
            }
        }
        public static List<EmployeeExperience> GetEmployeesAllExperience(int employeeId)
        {
            try
            {
                List<EmployeeExperience> pds = _employeeService.GetEmployeesAllExperience(1, 10, "", "JobTitle", "1", employeeId).Result;
                return pds;
            }
            catch
            {
                return null;
            }
        }

        #endregion Employee Experience

        #region Employee HR Administration

        public static EmployeeHRAdministration GetEmployeeHRAdministration(int employeeId)
        {
            try
            {
                EmployeeHRAdministration employeeHRAdministration = new EmployeeHRAdministration();
                employeeHRAdministration.EmployeeID = employeeId;

                var employeeAdministration = _employeeService.GetEmployeeHRAdministration(employeeId).Result;

                if (employeeAdministration != null)
                    employeeHRAdministration = employeeAdministration;

                return employeeHRAdministration;
            }
            catch
            {
                return null;
            }
        }

        public static SelectList GetAllCompanyAsync()
        {
            try
            {
                var bloodGroupMaster = _siteCompanyRepository.GetAllCompanyAsync(1, 10, "", "CompanyName", "1").Result;
                return new SelectList(bloodGroupMaster, "CompanyID", "CompanyName");
            }
            catch
            {
                return new SelectList(Enumerable.Empty<SelectListItem>());
            }
        }//Usp_GetAll_EmploymentStatusMaster

        public static SelectList GetAllEmploymentStatus()
        {
            try
            {
                var employeeStatus = _employeeService.GetAllEmploymentStatus();
                return new SelectList(employeeStatus, "EmploymentStatusID", "EmploymentStatusText");
            }
            catch
            {
                return new SelectList(Enumerable.Empty<SelectListItem>());
            }
        }

        public static SelectList GetAllEmploymentType()
        {
            try
            {
                var employeeType = _masterService.GetAllEmploymentTypeMaster();
                return new SelectList(employeeType, "EmploymentTypeID", "EmploymentTypeText");
            }
            catch
            {
                return new SelectList(Enumerable.Empty<SelectListItem>());
            }
        }

        public static SelectList GetAllEmployeeCategoryMaster()
        {
            try
            {
                var employeeCategory = _masterService.GetAllEmployeeCategoryMaster();
                return new SelectList(employeeCategory, "EmployeeCategoryID", "EmployeeCategoryText");
            }
            catch
            {
                return new SelectList(Enumerable.Empty<SelectListItem>());
            }
        }

        /*public static SelectList GetAllGetAllEmployeeCategoryMaster()
        {
            try
            {
                var employeeCategories = _employeeCategoryService.GetAllEmployeeCategoryAsync(0,0,"","","").Result;
                return new SelectList(employeeCategories, "EmployeeCategoryID", "EmployeeCategoryText");
            }
            catch
            {
                return new SelectList(Enumerable.Empty<SelectListItem>());
            }
        }*/

        #endregion Employee HR Administration

        #region Employee Salary BreakUp
        public static EmployeeSalaryBreakup GetEmployeeSalaryBreakup(int employeeId)
        {
            try
            {
                EmployeeSalaryBreakup employeeSalaryBreakup = new EmployeeSalaryBreakup();
                employeeSalaryBreakup.EmployeeID = employeeId;

                var employeeSalary = _employeeService.GetEmployeeSalaryBreakup(employeeId).Result;

                if (employeeSalary != null)
                    employeeSalaryBreakup = employeeSalary;

                return employeeSalaryBreakup;
            }
            catch
            {
                return null;
            }
        }
        #endregion Employee Salary BreakUp

        #region Employee Identity

        public static List<EmployeeIdentity> GetEmployeeAllIdentity(int employeeId)
        {
            try
            {
                List<EmployeeIdentity> pds = _employeeService.GetEmployeeAllIdentity(1, 10, "", "EmployeeID", "1", employeeId).Result;
                return pds;
            }
            catch
            {
                return null;
            }
        }

        public static SelectList GetAllIdentityTypeMaster()
        {
            try
            {
                var idProofTypes = _masterService.GetIdentityProofTypeAsync();
                return new SelectList(idProofTypes, "IdentityProofTypeID", "IdentityProofTypeText");
            }
            catch
            {
                return new SelectList(Enumerable.Empty<SelectListItem>());
            }
        }

        #endregion Employee Identity


        #region Employee Medical History
        public static List<EmployeeMedicalHistory> GetEmployeeAllMedicalHistory(int employeeId)
        {
            try
            {
                List<EmployeeMedicalHistory> pds = _employeeService.GetEmployeeAllMedicalHistory(1, 10, "", "EmployeeID", "1", employeeId).Result;
                return pds;
            }
            catch
            {
                return null;
            }
        }

        #endregion Employee Medical History

        #region Employee Medical Insurance

        public static List<EmployeeMedicalInsurance> GetEmployeeAllMedicalInsurance(int employeeId)
        {
            try
            {
                List<EmployeeMedicalInsurance> pds = _employeeService.GetEmployeeAllMedicalInsurance(1, 10, "", "EmployeeID", "1", employeeId).Result;
                return pds;
            }
            catch
            {
                return null;
            }
        }


        #endregion Employee Medical Insurance

        #region Contractor Name List

        public static SelectList GetAllContractorList()
        {
            try
            {
                var contractorList = _masterService.GetAllContractorList();
                return new SelectList(contractorList, "ContractorID", "ContractorName");
            }
            catch
            {
                return new SelectList(Enumerable.Empty<SelectListItem>());
            }
        }

        #endregion Contractor Name List
    }
}
