using Business.Entities.Employee;
using Business.Entities.Employee.EmployeeMedicalHistory;
using Business.Entities.Employee.EmployeeMedicalInsurance;
using Business.Entities.SalaryPaidHr;
using Business.Interface;
using Business.Interface.IEmployee;
using Business.Interface.ISalaryFormula;
using Business.SQL;
using ERP.Controllers;
using ERP.Extensions;
using ERP.Helpers;
using GridCore.Pagination;
using GridCore.Server;
using GridShared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading.Tasks;

namespace ERP.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize]
    /*[EncryptedActionParameter]*/
    public class EmployeeController : SettingsController
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMasterService _masterService;
        private readonly IConfiguration _configuration;
        private readonly IHostEnvironment _hostEnvironment;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string linkEmployeeImage;
        private readonly string linkEmployeeDocument;
        private readonly ISalaryFormula _salaryFormula;
        private readonly IDataProtector protector;


        public EmployeeController(IEmployeeService employeeService, IMasterService masterService, IConfiguration configuration, IHostEnvironment hostEnvironment, IWebHostEnvironment webHostEnvironment, ISalaryFormula salaryFormula, IDataProtectionProvider dataProtectionProvider, DataProtectionPurposeStrings dataProtectionPurposeStrings)
        {
            _employeeService = employeeService;
            _masterService = masterService;
            _configuration = configuration;
            linkEmployeeImage = _configuration.GetSection("EmployeeImagePath")["EmployeeImages"];
            linkEmployeeDocument = _configuration.GetSection("EmployeeImagePath")["EmployeeDocuments"];
            _hostEnvironment = hostEnvironment;
            _webHostEnvironment = webHostEnvironment;
            _salaryFormula = salaryFormula;
            this.protector = dataProtectionProvider.CreateProtector(dataProtectionPurposeStrings.EmployeeIdRouteValue);
        }


        public ActionResult Index([FromQuery(Name = "grid-page")] string gridpage = "1", [FromQuery(Name = "grid-column")] string orderby = "", [FromQuery(Name = "grid-dir")] string sortby = "0", [FromQuery(Name = "grid-filter")] string gridfilter = "", [FromQuery(Name = "grid-search")] string search = "")
        {
            int userid = USERID;
            IQueryCollection query = Request.Query;
            string value = string.Empty;
            Action<IGridColumnCollection<EmployeeMaster>> columns = c =>
            {
                c.Add(o => o.SrNo, "SrNo").Titled("Sr.No.").SetWidth(20);
                c.Add(o => o.EmployeeCode).Titled("Employee Code").Sortable(true);
                c.Add(o => o.EmployeeName).Titled("Name").Sortable(true);
                c.Add(o => o.GenderText).Titled("Gender").Sortable(false);
                c.Add(o => o.IsActive).Titled("IsActive").Sortable(true);
                //Below code hide on phones
                c.Add().Titled("Details").Encoded(false).Sanitized(false).SetWidth(60).Css("hidden-xs")
                .RenderValueAs(o => $"<a class='btn IndexPagebtnEidtPadding' href='/Admin/Employee/AddUpdateEmployee/{o.EncryptedId}' ><i class='bx bx-edit'></i></a>");
            };
            PagedDataTable<EmployeeMaster> pds = (PagedDataTable<EmployeeMaster>)_employeeService.GetAllEmployeesAsync(gridpage.ToInt(), PAGESIZE, search, orderby.RemoveSpace(), sortby == "0" ? "ASC" : "DESC").Result;
            foreach (var item in pds)
            {
                item.EncryptedId = protector.Protect(item.EmployeeID.ToString());
            }
            var server = new GridCoreServer<EmployeeMaster>(pds, query, false, "employeelistGrid", columns, PAGESIZE, pds.TotalItemCount)
                .Sortable()
                .Filterable(false)
                .Searchable(false, false)
                .Selectable(true)
                .WithGridItemsCount()
                .EmptyText("No record found")                
                .ClearFiltersButton(false)
                .WithPaging(PAGESIZE, pds.TotalItemCount, PAGESIZE, "grid-page");
                
            
            return View(server.Grid);
        }

        #region Basic details
        [HttpGet]
        public ActionResult AddUpdateEmployee(string id)
        {
            try
            {
                AddUpdateEmployee addUpdateEmployee = new AddUpdateEmployee();
                if (!string.IsNullOrEmpty(id))
                {
                    int decryptedIntId = 0;
                    // Decrypt the employee id using Unprotect method
                    decryptedIntId = Convert.ToInt32(protector.Unprotect(id));
                    if (decryptedIntId > 0)
                    {
                        addUpdateEmployee = _employeeService.GetEmployeeAsync(decryptedIntId).Result;
                        ViewData["Image"] = addUpdateEmployee.ImagePath;
                    }
                }
                var listDepartment = _masterService.GetAllDepartments();
                ViewData["DepartmentIdName"] = new SelectList(listDepartment, "DepartmentID", "DepartmentName");

                var listDesignation = _masterService.GetAllDesignations();
                ViewData["DesignationIdText"] = new SelectList(listDesignation, "DesignationID", "DesignationText");


                var listEmployees = _masterService.GetAllEmployees();
                ViewData["EmployeeIdName"] = new SelectList(listEmployees, "EmployeeID", "EmployeeName");

                var genders = _masterService.GetAllGenders();
                ViewData["GenderIdText"] = new SelectList(genders, "GenderID", "GenderText");

                var emailGroupMaster = _masterService.GetAllEmailGroupMaster();
                ViewData["EmailGroupName"] = new SelectList(emailGroupMaster, "EmailGroupID", "EmailGroupName");

                return View("AddUpdateEmployee", addUpdateEmployee);
            }
            catch (Exception ex)
            {
                /* _logger.LogError(ex, ex.Message);
                 //Response.Redirect("~/Home/Error.cshtml");
                 throw;*/

                return View("Error");
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddUpdateEmployee(AddUpdateEmployee addUpdateEmployee)
        {

            var path1 = "";
            addUpdateEmployee.CompanyID = COMPANYID;
            addUpdateEmployee.CreatedOrModifiedBy = USERID;
            addUpdateEmployee.EmployeeID = Convert.ToInt32(addUpdateEmployee.EmployeeID);
            var id = await _employeeService.AddUpdateEmployee(addUpdateEmployee);

            if (id > 0)
            {
                if (addUpdateEmployee.ProfilePhoto != null)
                {
                    string fileExtension = Path.GetExtension(addUpdateEmployee.ProfilePhoto.FileName);
                    // Add logic for save file in image folder. 29-09-2022.
                    EmployeeProfileImage employeeProfileImage = new EmployeeProfileImage();

                    path1 = _webHostEnvironment.WebRootPath + linkEmployeeImage;  //full path Excluding file name ----  0
                    string filepath = path1;  //full path including file name  -----  1
                    string filename = id + "_" + DateTime.Now.ToString().Replace("/", "").Replace(" ", "").Replace("-", "").Replace(":", "") + "_" + addUpdateEmployee.ProfilePhoto.FileName;
                    string dbfilepath = linkEmployeeImage + filename;
                    filepath = filepath + filename;

                    employeeProfileImage.EmployeeID = id;
                    employeeProfileImage.ImageName = filename;
                    employeeProfileImage.ImagePath = dbfilepath;
                    employeeProfileImage.CreatedOrModifiedBy = USERID;
                    employeeProfileImage.IsActive = true;

                    if (!Directory.Exists(path1))
                    {
                        Directory.CreateDirectory(path1);
                    }

                    if (Directory.Exists(path1))
                    {
                        using (var fileStream = new FileStream(filepath, FileMode.Create, FileAccess.ReadWrite))
                        {
                            addUpdateEmployee.ProfilePhoto.CopyTo(fileStream);
                        }
                        var profilePhotoId = _employeeService.UpdateEmployeeProfilePhoto(employeeProfileImage).Result;

                        var EncryptedId = protector.Protect(id.ToString());
                        //return Json(new { status = false, message = MessageHelper.Error });
                        return RedirectToAction("AddUpdateEmployee", new { id = EncryptedId });
                    }
                }
                var EncryID = protector.Protect(id.ToString());
                //return Json(new { status = true, message = MessageHelper.Added });
                return RedirectToAction("AddUpdateEmployee", new { id = EncryID });
            }
            else
            {
                //return Json(new { status = false, message = MessageHelper.Error });
                return RedirectToAction("AddUpdateEmployee");
            }
        }
        #endregion Basic details

        #region Address details
        [HttpGet]
        public async Task<PartialViewResult> AddUpdateEmployeeAddress(int employeeAddressTxnId, int employeeId)
        {
            try
            {
                EmployeeAddressTxn addressMaster = new EmployeeAddressTxn();
                addressMaster.EmployeeID = employeeId;
                if (employeeAddressTxnId > 0 || employeeId > 0)
                {
                    var employeeAddressTxn = await _employeeService.GetEmployeeAddressTxn(employeeAddressTxnId, employeeId);
                    if (employeeAddressTxn == null)
                        addressMaster.EmployeeID = employeeId;
                    else
                        addressMaster = employeeAddressTxn;

                    return PartialView("_addUpdateEmployeeAddress", addressMaster);
                }
                else
                    return PartialView("_addUpdateEmployeeAddress", addressMaster);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddUpdateEmployeeAddress(EmployeeAddressTxn addressMaster)
        {
            try
            {
                int addressId = await _employeeService.CreateOrUpdateEmployeeAddressAsync(addressMaster);
                if (addressId > 0)
                {
                    //return RedirectToAction("AddUpdateEmployee", new { id = addressMaster.EmployeeID });
                    return Json(new { status = true, message = MessageHelper.Added });
                }
                else
                    return Json(new { status = false, message = MessageHelper.Error });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
        #endregion Address details

        #region Family background

        [HttpPost]
        public async Task<ActionResult> AddUpdateEmployeeFamilyBackground(EmployeeFamilyDetail employeeFamilyDetail)
        {
            try
            {
                employeeFamilyDetail.CreatedModifiedBy = USERID;
                int familyId = await _employeeService.CreateOrUpdateEmployeeFamilyBackgroundAsync(employeeFamilyDetail);
                if (familyId > 0)
                {
                    return Json(new { status = true, message = MessageHelper.Added });
                }
                else
                    return Json(new { status = false, message = MessageHelper.Error });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        #endregion Family background

        #region Employee banking detail

        [HttpGet]
        public async Task<PartialViewResult> AddUpdateEmployeeBankAccount(int employeeBankDetailId, int employeeId)
        {
            try
            {
                EmployeeBankDetails employeeBankDetail = await _employeeService.GetEmployeeBankAccount(employeeBankDetailId, employeeId);

                if (employeeBankDetail == null)
                    employeeBankDetail = new EmployeeBankDetails { EmployeeID = employeeId };

                return PartialView("_addUpdateEmployeeBankAccount", employeeBankDetail);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddUpdateEmployeeBankAccount(EmployeeBankDetails employeeBankDetails)
        {
            try
            {
                employeeBankDetails.CreatedOrModifiedBy = USERID;
                int familyId = await _employeeService.CreateOrUpdateEmployeeBankDetail(employeeBankDetails);
                if (familyId > 0)
                {
                    return Json(new { status = true, message = MessageHelper.Added });
                }
                else
                    return Json(new { status = false, message = MessageHelper.Error });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
        #endregion Employee banking detail

        #region Employee document
        [HttpGet]
        public async Task<PartialViewResult> AddUpdateEmployeeDocument(int employeeDocumentId, int employeeId)
        {
            try
            {
                EmployeeDocument employeeDocument = await _employeeService.GetEmployeeDocument(employeeDocumentId, employeeId);

                if (employeeDocument == null)
                    employeeDocument = new EmployeeDocument { EmployeeID = employeeId };

                ViewData["DocumentPath"] = employeeDocument.DocumentPath;

                return PartialView("_addUpdateEmployeeDocument", employeeDocument);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddUpdateEmployeeDocument(EmployeeDocument employeeDocument)
        {
            try
            {
                string EncryID = string.Empty;
                if (employeeDocument.EmployeeID > 0)
                {
                    EncryID = protector.Protect(employeeDocument.EmployeeID.ToString());
                }
                int id = employeeDocument.EmployeeID;
                if (employeeDocument.DocumentFile != null)
                {
                    var path1 = "";
                    string fileExtension = Path.GetExtension(employeeDocument.DocumentFile.FileName);

                    path1 = _webHostEnvironment.WebRootPath + linkEmployeeDocument;  //full path Excluding file name ----  0
                    string filepath = path1;  //full path including file name  -----  1
                    string filename = employeeDocument.DocumentName.Replace("/", "").Replace(" ", "").Replace("-", "").Replace(":", "") + fileExtension;
                    string dbfilepath = linkEmployeeDocument + filename;
                    filepath = filepath + filename;
                    if (!Directory.Exists(path1))
                    {
                        Directory.CreateDirectory(path1);
                    }
                    if (Directory.Exists(path1))
                    {
                        using (var fileStream = new FileStream(filepath, FileMode.Create, FileAccess.ReadWrite))
                        {
                            employeeDocument.DocumentFile.CopyTo(fileStream);
                        }
                        employeeDocument.DocumentName = filename;
                        employeeDocument.DocumentExtension = fileExtension;
                        employeeDocument.DocumentPath = dbfilepath;
                        int documentId = await _employeeService.CreateOrUpdateEmployeeDocument(employeeDocument);
                        if (documentId > 0)
                        {
                            //TempData["message"] = MessageHelper.Uploaded;
                            //ViewBag.Message = MessageHelper.Uploaded;
                            //return Json(new { status = true, message = MessageHelper.Uploaded });
                            return RedirectToAction("AddUpdateEmployee", new { id = EncryID });
                        }
                        else
                        {
                            //ViewBag.Message = MessageHelper.Uploaded;
                            //return Json(new { status = false, message = MessageHelper.NoDocument });
                            return RedirectToAction("AddUpdateEmployee", new { id = EncryID });
                        }
                    }
                }
                //ViewBag.Message = MessageHelper.NoDocument;
                //return Json(new { status = false, message = MessageHelper.Error });
                return RedirectToAction("Index", "Employee");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult> ActiveInActiveEmployeeDocument(int employeeDocumentId, int employeeId, bool isActive)
        {
            EmployeeDocument employeeDocument = new EmployeeDocument()
            {
                EmployeeDocumentID = employeeDocumentId,
                EmployeeID = employeeId,
                IsActive = isActive,
                CreatedOrModifiedBy = USERID
            };
            int modifiedBy = USERID;
            int employeeDocumentIsActive = await _employeeService.ActiveInActiveEmployeeDocument(employeeDocument);
            if (employeeDocumentIsActive > 0)
            {
                if (isActive)
                    return Json(new { status = true, message = MessageHelper.ActivatedDocument });
                else
                    return Json(new { status = true, message = MessageHelper.InactivatedDocument });
            }
            else
                return Json(new { status = true, message = MessageHelper.Error });
        }
        #endregion Employee document

        #region Employee Education

        [HttpGet]
        public async Task<PartialViewResult> AddUpdateEmployeeEducation(int employeeEducationId, int employeeId)
        {
            try
            {
                EmployeeEducation employeeEducation = await _employeeService.AddUpdateEmployeeEducation(employeeEducationId, employeeId);

                if (employeeEducation == null)
                    employeeEducation = new EmployeeEducation { EmployeeID = employeeId };

                return PartialView("_addUpdateEmployeeEducation", employeeEducation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddUpdateEmployeeEducation(EmployeeEducation employeeEducation)
        {
            try
            {
                employeeEducation.CreatedOrModifiedBy = USERID;
                int educationId = await _employeeService.AddUpdateEmployeeEducation(employeeEducation);
                if (educationId > 0)
                {
                    return Json(new { status = true, message = MessageHelper.Added });
                }
                else
                    return Json(new { status = false, message = MessageHelper.Error });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        #endregion Employee Education

        #region Employee Experience

        [HttpGet]
        public async Task<PartialViewResult> AddUpdateEmployeeExperience(int employeeExperienceId, int employeeId)
        {
            try
            {
                EmployeeExperience employeeExperience = await _employeeService.AddUpdateEmployeeExperience(employeeExperienceId, employeeId);

                if (employeeExperience == null)
                    employeeExperience = new EmployeeExperience { EmployeeID = employeeId };

                return PartialView("_addUpdateEmployeeExperience", employeeExperience);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddUpdateEmployeeExperience(EmployeeExperience employeeExperience)
        {
            try
            {
                employeeExperience.CreatedOrModifiedBy = USERID;
                int ExperienceId = await _employeeService.AddUpdateEmployeeExperience(employeeExperience);
                if (ExperienceId > 0)
                {
                    return Json(new { status = true, message = MessageHelper.Added });
                }
                else
                    return Json(new { status = false, message = MessageHelper.Error });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        #endregion Employee Experience

        #region Employee HR Administration

        public async Task<ActionResult> AddUpdateEmployeeHRAdministration(EmployeeHRAdministration employeeHRAdministration)
        {
            try
            {
                employeeHRAdministration.CreatedOrModifiedBy = USERID;
                int adminId = await _employeeService.AddUpdateEmployeeHRAdministration(employeeHRAdministration);
                if (adminId > 0)
                {
                    return Json(new { status = true, message = MessageHelper.Added });
                }
                else
                    return Json(new { status = false, message = MessageHelper.Error });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        #endregion Employee HR Administration

        #region Employee Salary Breakup

        //[HttpGet]
        //public async Task<PartialViewResult> EmployeeSalaryBreakUp()
        //{
        //    try
        //    {
        //        EmployeeSalaryBreakup employeeSalaryBreakup = new EmployeeSalaryBreakup();
        //        employeeSalaryBreakup.EmployeeID = 2;
        //        return PartialView("_employeeSalaryBreakup", employeeSalaryBreakup);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, ex.Message);
        //        throw;
        //    }
        //}

        public async Task<ActionResult> AddUpdateEmployeeSalaryBreakup(EmployeeSalaryBreakup employeeSalaryBreakup)
        {
            try
            {
                employeeSalaryBreakup.CreatedOrModifiedBy = USERID;
                int salaryId = await _employeeService.AddUpdateEmployeeSalaryBreakup(employeeSalaryBreakup);
                if (salaryId > 0)
                {
                    return Json(new { status = true, message = MessageHelper.Added });
                }
                else
                    return Json(new { status = false, message = MessageHelper.Error });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
        #endregion Employee Salary Breakup

        #region Employee Identity

        [HttpGet]
        public async Task<PartialViewResult> AddUpdateEmployeeIdentity(int employeeIdentityID, int employeeId)
        {
            try
            {
                EmployeeIdentity employeeIdentity = await _employeeService.AddUpdateEmployeeIdentity(employeeIdentityID, employeeId);

                if (employeeIdentity == null)
                    employeeIdentity = new EmployeeIdentity { EmployeeID = employeeId };

                return PartialView("_addUpdateEmployeeIdentity", employeeIdentity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddUpdateEmployeeIdentity(EmployeeIdentity employeeIdentity)
        {
            try
            {
                employeeIdentity.CreatedOrModifiedBy = USERID;
                int IdentityId = await _employeeService.AddUpdateEmployeeIdentity(employeeIdentity);
                if (IdentityId > 0)
                {
                    return Json(new { status = true, message = MessageHelper.Added });
                }
                else
                    return Json(new { status = false, message = MessageHelper.Error });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        #endregion Employee Identity

        #region Employee Medical History

        [HttpGet]
        public async Task<PartialViewResult> AddUpdateEmployeeMedicalHistory(int employeeMedicalDetailsID, int employeeId)
        {
            try
            {
                EmployeeMedicalHistory employeeMedicalHistory = await _employeeService.AddUpdateEmployeeMedicalHistory(employeeMedicalDetailsID, employeeId);

                if (employeeMedicalHistory == null)
                    employeeMedicalHistory = new EmployeeMedicalHistory { EmployeeID = employeeId };

                return PartialView("Medical History/_addUpdateMedicalHistory", employeeMedicalHistory);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddUpdateEmployeeMedicalHistory(EmployeeMedicalHistory employeeMedicalHistory)
        {
            try
            {
                employeeMedicalHistory.CreatedOrModifiedBy = USERID;
                int EmployeeMedicalDetailsID = await _employeeService.AddUpdateEmployeeMedicalHistory(employeeMedicalHistory);
                if (EmployeeMedicalDetailsID > 0)
                {
                    return Json(new { status = true, message = MessageHelper.Added });
                }
                else
                    return Json(new { status = false, message = MessageHelper.Error });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        #endregion Employee Medical History

        #region Employee Medical Insurance

        [HttpGet]
        public async Task<PartialViewResult> AddUpdateEmployeeMedicalInsurance(int employeeMedicalInsuranceId, int employeeId)
        {
            try
            {
                EmployeeMedicalInsurance employeeMedicalInsurance = await _employeeService.AddUpdateEmployeeMedicalInsurance(employeeMedicalInsuranceId, employeeId);

                if (employeeMedicalInsurance == null)
                    employeeMedicalInsurance = new EmployeeMedicalInsurance { EmployeeID = employeeId };

                return PartialView("Medical Insurance/_addUpdateMedicalInsurance", employeeMedicalInsurance);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddUpdateEmployeeMedicalInsurance(EmployeeMedicalInsurance employeeMedicalInsurance)
        {
            try
            {
                employeeMedicalInsurance.CreatedOrModifiedBy = USERID;
                int EmployeeMedicalInsuranceID = await _employeeService.AddUpdateEmployeeMedicalInsurance(employeeMedicalInsurance);
                if (EmployeeMedicalInsuranceID > 0)
                {
                    return Json(new { status = true, message = MessageHelper.Added });
                }
                else
                    return Json(new { status = false, message = MessageHelper.Error });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        #endregion Employee Medical Insurance

        #region Employee salary calculation  // Employee salary calculation is hold on 23-Jan-2023.
        public async Task<PartialViewResult> EmployeeSalaryCalculation(int employeeId)
        {
            try
            {
                EmployeeSalaryBreakup employeeSalaryBreakup = new EmployeeSalaryBreakup();
                var salaryFormulaMaster = _salaryFormula.GetSalaryFormula().Result;

                var employeeCtc = EmployeeExtension.GetEmployeeSalaryBreakup(employeeId).CostToCompany;

                #region Calculation part

                int basicPay = Convert.ToInt32(employeeCtc) * salaryFormulaMaster.Basic / 100;

                #endregion Calculation part

                employeeSalaryBreakup.Basic = Convert.ToString(basicPay);
                return PartialView("_employeeSalaryBreakup", employeeSalaryBreakup);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
        #endregion Employee salary calculation

        #region Employee Present

        [HttpGet]
        public ActionResult GetEmployeePresent(int companyId, int departmentId, DateTime? presentDate, string serachString)
        {
            try
            {

                if (presentDate == null)
                    presentDate = DateTime.Now.Date;
                //string serachString = string.Empty;
                EmployeePresent employeepresent = new EmployeePresent();
                List<EmployeePresentList> employeePresentList2 = new List<EmployeePresentList>();

                var listDepartment = _masterService.GetAllDepartments();
                ViewData["DepartmentIdName"] = new SelectList(listDepartment, "DepartmentID", "DepartmentName");

                int UID = USERID;
                /*  var DepartmentList = _masterService.GetAllDepartments();
                  ViewData["DepartmentText"] = new SelectList(DepartmentList, "DepartmentID", "DepartmentText");*/

                var employeeeList = _masterService.GetAllEmployeesTimeSheet(companyId, departmentId, presentDate, serachString, UID);

                foreach (var employeeList in employeeeList)
                {
                    EmployeePresentList employeePresentList1 = new EmployeePresentList();

                    employeePresentList1.EmployeeID = employeeList.EmployeeID;
                    employeePresentList1.EmployeeName = employeeList.EmployeeName;
                    employeePresentList1.DepartmentID = employeeList.DepartmentID;
                    employeePresentList1.DepartmentName = employeeList.DepartmentName;
                    employeePresentList1.EmployeeTimeSheetDetailID = employeeList.EmployeeTimeSheetDetailID;
                    employeePresentList1.EmployeeTimeSheetMasterID = employeeList.EmployeeTimeSheetMasterID;
                    employeePresentList1.SrNo = employeeList.SrNo;
                    employeePresentList1.PresenceDate = presentDate.Value;
                    employeePresentList1.InTime = employeeList.InTime;
                    employeePresentList1.OutTime = employeeList.OutTime;
                    employeePresentList1.BreakHour = employeeList.BreakHour;
                    employeePresentList1.Overtime = employeeList.Overtime;
                    employeePresentList2.Add(employeePresentList1);
                }
                employeepresent.CompanyID = COMPANYID;
                employeepresent.getEmployeePresentList = employeePresentList2;

                if (companyId > 0 || departmentId > 0)
                {
                    employeepresent.DepartmentID = departmentId;
                    employeepresent.CompanyID = companyId;
                    employeepresent.PresenceDateTime = presentDate.Value;
                    return Json(employeepresent);
                }

                employeepresent.PresenceDateTime = presentDate.Value;
                return View("GetEmployeePresent", employeepresent);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
        [HttpPost]
        public ActionResult AddUpdateEmployeePresent(EmployeePresent employeePresent)
        {
            if (employeePresent != null)
                return Json(new { status = true, employeePresent });
            else
                return Json(new { status = true, MessageHelper.Error });
        }

        [HttpPost]
        public ActionResult AddUpdateEmployeePresentList(List<EmployeePresentList> employeePresentLists, EmployeePresent employeePresent)
        {
            try
            {
                employeePresent.CreatedOrModifiedBy = USERID;
                //   int employeeTimeSheetMasterID = _employeeService.AddUpdateEmployeePresent(employeePresent, null).Result;

                //if (employeeTimeSheetMasterID > 0)
                //{

                DataTable dataTable = new DataTable();
                dataTable.Columns.Add(new DataColumn("EmployeeTimeSheetDetailID", typeof(int)));
                dataTable.Columns.Add(new DataColumn("EmployeeTimeSheetMasterID", typeof(int)));
                dataTable.Columns.Add(new DataColumn("EmployeeID", typeof(int)));
                dataTable.Columns.Add(new DataColumn("PresenceDate", typeof(DateTime)));
                dataTable.Columns.Add(new DataColumn("InTime", typeof(string)));
                dataTable.Columns.Add(new DataColumn("OutTime", typeof(string)));
                dataTable.Columns.Add(new DataColumn("BreakupTime", typeof(string)));
                dataTable.Columns.Add(new DataColumn("Overtime", typeof(string)));

                foreach (var item in employeePresentLists)
                {
                    dataTable.Rows.Add(new object[]
                    {
                            item.EmployeeTimeSheetDetailID = 0,
                            item.EmployeeTimeSheetMasterID,
                            item.EmployeeID,
                            item.PresenceDate = employeePresent.PresenceDateTime,
                            item.InTime.ToShortTimeString(),
                            item.OutTime.ToShortTimeString(),
                            item.BreakHour,
                            item.Overtime
                    });
                }
                var employeeTimeSheetMasterID = _employeeService.AddUpdateEmployeePresent(employeePresent, dataTable).Result;
                return Json(new { status = true, MessageHelper.Updated });
                //}
                //else
                //    return Json(new { status = false, MessageHelper.Error });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        #endregion Employee Present

        #region Employee Name Search (for Typeheaad method)

        [HttpPost]
        public ActionResult SearchEmployeeDropdown(string prefix)
        {
            var employeeResult = _masterService.GetAllEmployeesTimeSheet(0, 0, null, prefix,0);

            return Json(new { status = true, employeeResult });
        }

        #endregion Employee Name Search (for Search employee in dropdown list- Typeheaad method)

        //#region test
        //public ActionResult EmployeeIndex()
        //{
        //    int userid = USERID;
        //    IQueryCollection query = Request.Query;
        //    string value = string.Empty;
        //    List<EmployeeMaster> pds = _employeeService.GetAllEmployeesAsync(1, 20, "", "EmployeeID", "ASC" == "0" ? "ASC" : "DESC").Result.Select(e =>
        //        {
        //            e.EncryptedId = protector.Protect(e.EmployeeID.ToString());
        //            return e;
        //        }).ToList();
        //    return View(pds);
        //}
        //[AllowAnonymous]
        //public ViewResult Details(string id)
        //{
        //    // Decrypt the employee id using Unprotect method
        //    string decryptedId = protector.Unprotect(id);
        //    int decryptedIntId = Convert.ToInt32(decryptedId);

        //    AddUpdateEmployee employee = _employeeService.GetEmployeeAsync(decryptedIntId).Result;

        //    return View(employee);
        //}
        //#endregion

        #region Employee Document Image Download
        public FileResult OpenImage(string fileName)
        {
            byte[] fileBytes = System.IO.File.ReadAllBytes(@_webHostEnvironment.WebRootPath + linkEmployeeDocument + fileName);
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }
        #endregion Employee Document Image Download

        #region Employee Salary Paid Hours

        [HttpGet]
        public async Task<PartialViewResult> AddUpdateEmployeeSalaryPaidHr(int employeeId)
        {
            try
            {
                var listAllUsersByCompanyId = _masterService.GetAllUsersByCompanyId(COMPANYID, 0, 0, "", 0, "", "");
                ViewData["UsersByCompanyId"] = new SelectList(listAllUsersByCompanyId, "UserID", "FullName");

                SalaryPaidHr employeeSalaryPaidHr = new SalaryPaidHr();
                var employeesalarypaidHr = await _employeeService.GetSalaryPaidHr(employeeId);

                if (employeesalarypaidHr == null)
                    employeeSalaryPaidHr.EmployeeID = employeeId;
                else
                    employeeSalaryPaidHr = employeesalarypaidHr;

                return PartialView("_addUpdateEmployeeSalaryPaidHr", employeeSalaryPaidHr);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddUpdateEmployeeSalaryPaidHr(SalaryPaidHr salaryPaidHr)
        {
            try
            {
                salaryPaidHr.CreatedOrModifiedBy = USERID;
                int salaryPaidHrId = await _employeeService.AddUpdateSalaryPaidHr(salaryPaidHr);
                if (salaryPaidHrId > 0)
                {
                    return Json(new { status = true, message = MessageHelper.Added });
                }
                else
                    return Json(new { status = false, message = MessageHelper.Error });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        #endregion Employee Salary Paid Hours


    }
}