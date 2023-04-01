using Business.Entities;
using Business.Entities.Company;
using Business.Entities.Contractor;
using Business.Interface;
using Business.SQL;
using DocumentFormat.OpenXml.Wordprocessing;
using ERP.Controllers;
using ERP.Helpers;
using GridCore.Server;
using GridMvc.Server;
using GridShared;
using GridShared.Sorting;
using GridShared.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
namespace ERP.Areas.SuperAdmin.Controllers
{


    [Area("SuperAdmin"), Authorize]
    [DisplayName("Company")]
    public class CompanyController : BaseController
    {


        #region "Variable"
        private readonly UserManager<UserMasterMetadata> _userManager;
        private readonly SignInManager<UserMasterMetadata> _signInManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ISiteCompanyRepository _companyManager;
        private readonly IConfiguration _configuration;
        private readonly string linkCompanyLogoImage;
        private readonly string linkCompanyDocument;
        private readonly IDataProtector protector;
        public CompanyController(ISiteCompanyRepository companyManager, UserManager<UserMasterMetadata> userManager, SignInManager<UserMasterMetadata> signInManager, IWebHostEnvironment hostEnvironment, IConfiguration configuration, IDataProtectionProvider dataProtectionProvider, DataProtectionPurposeStrings dataProtectionPurposeStrings)
        {

            this._userManager = userManager;
            this._signInManager = signInManager;
            _webHostEnvironment = hostEnvironment;
            _companyManager = companyManager;
            _configuration = configuration;
            linkCompanyLogoImage = _configuration.GetSection("CompanyLogoImagePath")["CompanyLogoImage"];
            linkCompanyDocument = _configuration.GetSection("CompanyLogoImagePath")["CompanyDocuments"];
            this.protector = dataProtectionProvider.CreateProtector(dataProtectionPurposeStrings.CompanyIdRouteValue);
        }

        private void Errors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
                ModelState.AddModelError("", error.Description);
        }
        #endregion

        #region "Add Company Slider"
        [HttpPost]
        public async Task<PartialViewResult> Get(int id)
        {

            try
            {
                CompanyMasterMetadata model = await _companyManager.GetCompanyAsync(id);
                if (model == null)
                    model = new CompanyMasterMetadata();
                return PartialView("Register", model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
        #endregion "Add Company Slider"

        #region"Company Slider Register"
        [HttpPost]
        public async Task<IActionResult> RegisterCompany(CompanyMasterMetadata model)
        {
            try
            {
                model.CreatedOrModifiedBy = USERID;
                if (model.ImageFile != null)
                {
                    string uniqueFileName = UploadedFile(model);
                    model.CompanyLogoImagePath = uniqueFileName;
                    model.CompanyLogoName = uniqueFileName;
                }

                int compnayID = _companyManager.CreateOrUpdateCompanyAsync(model).Result;
                if (compnayID > 0 && model.CompanyID == 0)
                {
                    model.CompanyID = compnayID;
                    var userCheck = await _userManager.FindByEmailAsync(model.Email);
                    if (userCheck == null)
                    {
                        var user = new UserMasterMetadata
                        {
                            Forename = model.FirstName,
                            Surname = model.LastName,
                            Username = model.Email,
                            NormalizedUserName = model.Email,
                            Email = model.Email,
                            PhoneNumber = model.Mobile,
                            EmailConfirmed = true,
                            PhoneNumberConfirmed = true,
                            CompanyID = model.CompanyID,
                            IsActive = true
                        };
                        var result = await _userManager.CreateAsync(user, model.Password);
                        if (result.Succeeded)
                        {
                            await _userManager.AddToRoleAsync(user, "Admin");
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            if (result.Errors.Count() > 0)
                            {
                                foreach (var error in result.Errors)
                                {
                                    ModelState.AddModelError("message", error.Description);
                                }
                            }
                            return View(model);
                        }
                    }
                }
                else
                {
                    return RedirectToAction("Index");
                }
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
        #endregion

        #region "Company"
        public ActionResult Index([FromQuery(Name = "grid-page")] string gridpage = "1", [FromQuery(Name = "grid-column")] string orderby = "", [FromQuery(Name = "grid-dir")] string sortby = "0", [FromQuery(Name = "grid-filter")] string gridfilter = "", [FromQuery(Name = "grid-search")] string search = "")
        {
            try
            {
                int userid = USERID;
                IQueryCollection query = Request.Query;

                Action<IGridColumnCollection<CompanyMasterMetadata>> columns = c =>
                {
                    c.Add(o => o.CompanyName)
                        .Titled("Company Name")
                        .SortInitialDirection(GridSortDirection.Ascending)
                        //.ThenSortByDescending(o => o.CompanyID)
                        .SetWidth(110);


                    c.Add(o => o.CompanyCode)
                        .Titled("Company Code")
                        .SetWidth(250);

                    c.Add()
                        .Encoded(false)
                        .Sanitized(false)
                        .SetWidth(60)
                        .RenderValueAs(o => $"<a class='btn' href='Company/Edit/{o.EncryptedId}' ><i class='bx bx-edit'></i></a>");


                };
                PagedDataTable<CompanyMasterMetadata> pds = _companyManager.GetAllCompanyAsync(gridpage.ToInt(), PAGESIZE, search, orderby.RemoveSpace(), sortby == "0" ? "ASC" : "DESC").Result;
                foreach (var item in pds)
                {
                    item.EncryptedId = protector.Protect(item.CompanyID.ToString());
                }
                var server = new GridCoreServer<CompanyMasterMetadata>(pds, query, false, "ordersGrid",
                    columns, PAGESIZE, pds.TotalItemCount)
                    //.Filterable()
                    //.WithMultipleFilters()
                    //.Groupable(true)
                    //.ClearFiltersButton(true)
                    //.Selectable(true)
                    //.Searchable()
                    .Sortable()
                    .SetStriped(true)
                    .ChangePageSize(true)
                    .WithGridItemsCount()
                    .WithPaging(PAGESIZE, pds.TotalItemCount)
                    .ChangeSkip(false)
                    .EmptyText("No record found");
                return View(server.Grid);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
        public IActionResult Register()
        {
            return View("RegisterOG", new CompanyMasterMetadata());
        }
        public IActionResult Edit(string id)
        {
            try
            {
                CompanyMasterMetadata companyMasterMetadata = new CompanyMasterMetadata();

                companyMasterMetadata.EncryptedId = id;
                if (id == null)
                {
                    return View("Edit", companyMasterMetadata);
                }
                else
                {
                    string decryptedId = string.Empty;
                    int decryptedIntId = 0;
                    // Decrypt the employee id using Unprotect method
                    if (!string.IsNullOrEmpty(id))
                    {
                        decryptedId = protector.Unprotect(id);
                        decryptedIntId = Convert.ToInt32(decryptedId);
                    }
                    if (decryptedIntId > 0)
                    {
                        companyMasterMetadata = _companyManager.GetCompnayAsync(decryptedIntId.ToString()).Result;
                        return View("Edit", companyMasterMetadata);
                    }
                    else
                    {
                        return View("Edit", companyMasterMetadata);
                    }
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }


        [HttpPost]
        public async Task<IActionResult> Register(CompanyMasterMetadata model)
        {
            try
            {
                model.CreatedOrModifiedBy = USERID;
                if (model.ImageFile != null)
                {
                    string uniqueFileName = UploadedFile(model);
                    model.CompanyLogoImagePath = uniqueFileName;
                    model.CompanyLogoName = uniqueFileName;
                }

                int compnayID = _companyManager.CreateOrUpdateCompanyAsync(model).Result;
                if (compnayID > 0 && model.CompanyID == 0)
                {
                    model.CompanyID = compnayID;
                    var userCheck = await _userManager.FindByEmailAsync(model.Email);
                    if (userCheck == null)
                    {
                        var user = new UserMasterMetadata
                        {
                            Forename = model.FirstName,
                            Surname = model.LastName,
                            Username = model.Email,
                            NormalizedUserName = model.Email,
                            Email = model.Email,
                            PhoneNumber = model.Mobile,
                            EmailConfirmed = true,
                            PhoneNumberConfirmed = true,
                            CompanyID = model.CompanyID,
                            IsActive = true
                        };
                        var result = await _userManager.CreateAsync(user, model.Password);
                        if (result.Succeeded)
                        {
                            await _userManager.AddToRoleAsync(user, "Admin");
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            if (result.Errors.Count() > 0)
                            {
                                foreach (var error in result.Errors)
                                {
                                    ModelState.AddModelError("message", error.Description);
                                }
                            }
                            return View(model);
                        }
                    }
                }
                else
                {
                    return RedirectToAction("Index");
                }
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
        private string UploadedFile(CompanyMasterMetadata model)
        {
            string filePath, fileName = null;
            string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "companylogo");
            if (!string.IsNullOrEmpty(model.CompanyLogoName))
            {
                filePath = Path.Combine(uploadsFolder, model.CompanyLogoName);
                if (System.IO.File.Exists(filePath))
                    System.IO.File.Delete(filePath);
            }
            if (model.ImageFile != null)
            {

                fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(model.ImageFile.FileName);
                filePath = Path.Combine(uploadsFolder, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ImageFile.CopyTo(fileStream);
                }
            }
            return fileName;
        }
        public async Task<IActionResult> ShowLogo()
        {
            try
            {
                CompanyLogoMasterMetadata model = null;
                model = await _companyManager.GetCompanyLogoMaster(COMPANYID);
                if (model != null)
                {
                    string filePath;
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "companylogo");
                    if (!string.IsNullOrEmpty(model.CompanyLogoName))
                    {
                        filePath = Path.Combine(uploadsFolder, model.CompanyLogoName);
                        if (System.IO.File.Exists(filePath))
                        {

                            byte[] bytes = System.IO.File.ReadAllBytes(filePath);
                            return new FileStreamResult(new System.IO.MemoryStream(bytes), "image/png");
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
        #endregion

        #region "Contact"

        [HttpGet]
        public async Task<PartialViewResult> AddContact(int id, int compnayid)
        {
            try
            {
                CompanyContactTxnMetadata model = await _companyManager.GetCompnayContactAsync(compnayid, id);
                if (model == null)
                    model = new CompanyContactTxnMetadata() { CompanyID = compnayid };
                return PartialView("_addContact", model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> SaveContact(CompanyContactTxnMetadata model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //model.CompanyID = COMPANYID;
                    int i = await _companyManager.CreateOrUpdateCompanyContactAsync(model);
                    if (i > 0)
                    {
                        if (model.CompanyContactPersonsID == 0)
                            return Json(new { status = true, message = MessageHelper.Added });
                        else
                            return Json(new { status = true, message = MessageHelper.Updated });
                    }
                }
                return Json(new { status = false, message = MessageHelper.Error });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Json(new { status = false, message = MessageHelper.Error });
            }
        }
        #endregion

        #region "Address"

        [HttpGet]
        public async Task<PartialViewResult> AddAddress(int id, int compnayid)
        {
            try
            {
                CompanyAddressTxnMetadata model = await _companyManager.GetCompnayAddressAsync(compnayid, id);
                if (model == null)
                    model = new CompanyAddressTxnMetadata() { CompanyID = compnayid };
                return PartialView("_addAddress", model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
        [HttpPost]
        public async Task<IActionResult> SaveAddress(CompanyAddressTxnMetadata model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.CreatedOrModifiedBy = USERID;
                    int i = await _companyManager.CreateOrUpdateCompanyAddressAsync(model);
                    if (i > 0)
                    {
                        if (model.CompanyAddressTxnID == 0)
                            return Json(new { status = true, message = MessageHelper.Added });
                        else
                            return Json(new { status = true, message = MessageHelper.Updated });
                    }
                }
                return Json(new { status = false, message = MessageHelper.Error });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Json(new { status = false, message = MessageHelper.Error });
            }
        }
        #endregion

        #region "Bank Details"

        [HttpGet]
        public async Task<PartialViewResult> AddUpdateCompanyBankAccount(int CompanyBankDetailId, int CompanyId)
        {
            try
            {
                CompanyBankDetails CompanyBankDetails = new CompanyBankDetails();
                CompanyBankDetails.CompanyID = CompanyId;
                if (CompanyBankDetailId > 0 || CompanyId > 0)
                {
                    var CompanyBankdetail = await _companyManager.GetCompanyBankAccount(CompanyBankDetailId, CompanyId);
                    if (CompanyBankdetail == null)
                        CompanyBankDetails.CompanyID = CompanyId;
                    else
                        CompanyBankDetails = CompanyBankdetail;

                    return PartialView("_AddUpdateCompanyBankAccount", CompanyBankDetails);
                }
                else
                    return PartialView("_AddUpdateCompanyBankAccount", CompanyBankDetails);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddUpdateCompanyBankAccount(CompanyBankDetails CompanyBankDetails)
        {
            try
            {
                CompanyBankDetails.CreatedOrModifiedBy = USERID;
                int CompanyBankAccountId = await _companyManager.AddUpdateCompanyBankDetails(CompanyBankDetails);
                if (CompanyBankAccountId > 0)
                {
                    //return RedirectToAction("AddUpdateCompany", new { id = CompanyBankDetails.CompanyID });
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

        #endregion "Bank Details"

        #region "Registration"

        [HttpPost]
        public async Task<ActionResult> AddUpdateCompanyRegistration(CompanyRegistration CompanyRegistration)
        {
            try
            {
                CompanyRegistration.CreatedOrModifiedBy = USERID;
                int registrationId = await _companyManager.AddUpdateCompanyRegistration(CompanyRegistration);
                if (registrationId > 0)
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

        #endregion "Registration" 

        #region Company Document
        [HttpGet]
        public async Task<PartialViewResult> AddUpdateCompanyDocument(int CompanyDocumentId, int CompanyId)
        {
            try
            {
                CompanyDocument CompanyDocument = await _companyManager.GetCompanyDocument(CompanyDocumentId, CompanyId);

                if (CompanyDocument == null)
                {
                    CompanyDocument = new CompanyDocument { CompanyID = CompanyId, };
                }
                ViewData["DocumentPath"] = CompanyDocument.DocumentPath;

                return PartialView("_AddUpdateCompanyDocument", CompanyDocument);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddUpdateCompanyDocument(CompanyDocument CompanyDocument)
        {
            try
            {
                string EncryID = string.Empty;
                if (CompanyDocument.CompanyID > 0)
                {
                    EncryID = protector.Protect(CompanyDocument.CompanyID.ToString());
                }
                int id = CompanyDocument.CompanyID;
                if (CompanyDocument.DocumentFile != null)
                {
                    var path1 = "";
                    string fileExtension = Path.GetExtension(CompanyDocument.DocumentFile.FileName);

                    path1 = _webHostEnvironment.WebRootPath + linkCompanyDocument;  //full path Excluding file name ----  0
                    string filepath = path1;  //full path including file name  -----  1
                    string filename = CompanyDocument.DocumentName.Replace("/", "").Replace(" ", "").Replace("-", "").Replace(":", "").Replace(".jpg", "").Replace(".png", "").Replace(".jpeg", "").Replace(".gif", "").Replace(".EPS", "").Replace(".AI", "").Replace(".PDF", "")
                        + fileExtension;
                    string dbfilepath = linkCompanyDocument + filename;
                    filepath = filepath + filename;

                    if (!Directory.Exists(path1))
                    {
                        Directory.CreateDirectory(path1);
                    }

                    if (Directory.Exists(path1))
                    {
                        using (var fileStream = new FileStream(filepath, FileMode.Create, FileAccess.ReadWrite))
                        {
                            CompanyDocument.DocumentFile.CopyTo(fileStream);
                        }
                        CompanyDocument.DocumentName = filename;
                        CompanyDocument.DocumentExtension = fileExtension;
                        CompanyDocument.DocumentPath = dbfilepath;
                        int documentId = await _companyManager.AddUpdateCompanyDocument(CompanyDocument);
                        if (documentId > 0)
                        {
                            //TempData["message"] = MessageHelper.Uploaded;
                            //ViewBag.Message = MessageHelper.Uploaded;
                            //return Json(new { status = true, message = MessageHelper.Uploaded });
                            return RedirectToAction("Edit", new { id = EncryID });
                        }
                        else
                        {
                            //ViewBag.Message = MessageHelper.Uploaded;
                            //return Json(new { status = false, message = MessageHelper.NoDocument });
                            return RedirectToAction("Edit", new { id = EncryID });
                        }
                    }
                }
                //ViewBag.Message = MessageHelper.NoDocument;
                //return Json(new { status = false, message = MessageHelper.Error });
                return RedirectToAction("Edit", new { id = EncryID });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }


        #endregion Company Document

        #region Company Document Image Download      

        public FileResult OpenImage(string fileName)
        {
            byte[] fileBytes = System.IO.File.ReadAllBytes(@_webHostEnvironment.WebRootPath + linkCompanyDocument + fileName);
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }
        #endregion Company Document Image Download
    }
}