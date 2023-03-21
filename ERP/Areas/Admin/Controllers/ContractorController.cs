using Business.Interface;
using ERP.Controllers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Threading.Tasks;
using Business.Entities.Contractor;
using Business.Interface.IContractor;
using PartialViewResult = Microsoft.AspNetCore.Mvc.PartialViewResult;
using ActionResult = Microsoft.AspNetCore.Mvc.ActionResult;
using HttpPostAttribute = System.Web.Mvc.HttpPostAttribute;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using AuthorizeAttribute = Microsoft.AspNetCore.Authorization.AuthorizeAttribute;
using Business.SQL;
using Microsoft.AspNetCore.Http;
using GridShared;
using GridCore.Server;
using ERP.Helpers;
using JsonResult = Microsoft.AspNetCore.Mvc.JsonResult;
using System.ComponentModel;
using Business.Entities.Employee;
using Microsoft.AspNetCore.DataProtection;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace ERP.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize]
    [DisplayName("Contractor")]
    public class ContractorController : SettingsController
    {
        private readonly IMasterService _masterService;
        private readonly IContractorService _contractorService;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string linkContractorLogoImage;
        private readonly string linkContractorDocument;
        private readonly IDataProtector protector;

        public ContractorController(IMasterService masterService, IContractorService contractorService, IConfiguration configuration, IWebHostEnvironment webHostEnvironment, IDataProtectionProvider dataProtectionProvider, DataProtectionPurposeStrings dataProtectionPurposeStrings)
        {
            _masterService = masterService;
            _contractorService = contractorService;
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
            linkContractorLogoImage = _configuration.GetSection("ContractorLogoImagePath")["ContractorLogoImage"];
            linkContractorDocument = _configuration.GetSection("ContractorLogoImagePath")["ContractorDocuments"];
            this.protector = dataProtectionProvider.CreateProtector(dataProtectionPurposeStrings.ContractorIdRouteValue);
        }
        public IActionResult Index([FromQuery(Name = "grid-page")] string gridpage = "1", [FromQuery(Name = "grid-column")] string orderby = "", [FromQuery(Name = "grid-dir")] string sortby = "0", [FromQuery(Name = "grid-filter")] string gridfilter = "", [FromQuery(Name = "grid-search")] string search = "")
        {
            int userid = USERID;
            IQueryCollection query = Request.Query;
            string value = string.Empty;
            Action<IGridColumnCollection<ContractorMaster>> columns = c =>
            {
                c.Add(o => o.SrNo, "SrNo").Titled("Sr.No.").SetWidth(20);
                c.Add(o => o.ContractorCode).Titled("Contractor Code").Sortable(true);
                c.Add(o => o.ContractorName).Titled("Contractor Name").Sortable(true);
                c.Add(o => o.GroupName).Titled("Group Name").Sortable(true);
                c.Add(o => o.OwnerName).Titled("Owner Name").Sortable(true);
                c.Add(o => o.IsActive).Titled("IsActive").Sortable(true);
                //Below code hide on phones
                c.Add().Titled("Details").Encoded(false).Sanitized(false).SetWidth(60).Css("hidden-xs")
                .RenderValueAs(o => $"<a class='btn IndexPagebtnEidtPadding' href='/Admin/Contractor/AddUpdateContractor/{o.EncryptedId}' ><i class='bx bx-edit'></i></a>");
            };
            PagedDataTable<ContractorMaster> pds = _contractorService.GetAllContractorAsync(gridpage.ToInt(), PAGESIZE, search, orderby.RemoveSpace(), sortby == "0" ? "ASC" : "DESC").Result;
            foreach (var item in pds)
            {
                item.EncryptedId = protector.Protect(item.ContractorID.ToString());
            }
            var server = new GridCoreServer<ContractorMaster>(pds, query, false, "ordersGrid",
                columns, PAGESIZE, pds.TotalItemCount)
                
                .Sortable(true)
                .ClearFiltersButton(true)
                .Selectable(true)
                .WithGridItemsCount()
                .ChangeSkip(false)
                .EmptyText("No record found")
                .Searchable(true,false)
                .ClearFiltersButton(false);
                
            return View(server.Grid);

            /*return View();*/
        }

        #region Contractor Basic Details

        [HttpGet]
        public ActionResult AddUpdateContractor(string id)
        {
            try
            {
                ContractorMaster contractorMaster = new ContractorMaster();
                contractorMaster.EncryptedId = id;
                if (id == null)
                {

                    ViewData["LogoImage"] = contractorMaster.LogoImagePath;
                    return PartialView("AddUpdateContractor/AddUpdateContractor", contractorMaster);
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
                        contractorMaster = _contractorService.GetContractorAsync(decryptedIntId).Result;

                        ViewData["LogoImage"] = contractorMaster.LogoImagePath;

                        return PartialView("AddUpdateContractor/AddUpdateContractor", contractorMaster);
                    }
                    else
                    {
                        return PartialView("AddUpdateContractor/AddUpdateContractor", contractorMaster);
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
        public async Task<ActionResult> AddUpdateContractor(ContractorMaster contractorMaster)
        {
            var path1 = "";
            contractorMaster.CreatedOrModifiedBy = USERID;
            var id = await _contractorService.AddUpdateContractor(contractorMaster);

            if (id > 0)
            {
                if (contractorMaster.LogoImage != null)
                {
                    string fileExtension = Path.GetExtension(contractorMaster.LogoImage.FileName);
                    //Add logic for save file in image folder. 29-09-2022.
                    LeadLogoImage contractorLogoImage = new LeadLogoImage();
                    path1 = _webHostEnvironment.WebRootPath + linkContractorLogoImage;  //full path Excluding file name ----  0
                    string filepath = path1;  //full path including file name  -----  1
                    string filename = id + "_" + DateTime.Now.ToString().Replace("/", "").Replace(" ", "").Replace("-", "").Replace(":", "") + "_" + contractorMaster.LogoImage.FileName;
                    string dbfilepath = linkContractorLogoImage + filename;
                    filepath = filepath + filename;
                    contractorLogoImage.ContractorID = id;
                    contractorLogoImage.LogoImageName = filename;
                    contractorLogoImage.LogoImagePath = dbfilepath;
                    contractorLogoImage.CreatedOrModifiedBy = USERID;
                    contractorLogoImage.IsActive = true;

                    if (!Directory.Exists(path1))
                    {
                        Directory.CreateDirectory(path1);
                    }
                    if (Directory.Exists(path1))
                    {
                        using (var fileStream = new FileStream(filepath, FileMode.Create, FileAccess.ReadWrite))
                        {
                            contractorMaster.LogoImage.CopyTo(fileStream);
                        }
                        var profilePhotoId = _contractorService.UpdateContractorLogoImage(contractorLogoImage).Result;
                        //return Json(new { status = false, message = MessageHelper.Error });
                        return RedirectToAction("AddUpdateContractor", "Contractor", new { id = id });
                    }
                }
                var EncryID = protector.Protect(id.ToString());
                return RedirectToAction("AddUpdateContractor", "Contractor", new { id = EncryID });

            }
            else
            {
                return RedirectToAction("AddUpdateContractor", "Contractor");
            }
        }

        #endregion Contractor Basic Details

        #region Contractor Contact Person List

        [HttpGet]
        public async Task<PartialViewResult> AddUpdateContractorContactPerson(int ContractorContactID, int ContractorId)
        {
            try
            {
                ContractorContactTxn ContractorContactTxn = new ContractorContactTxn();
                ContractorContactTxn.ContractorID = ContractorId;
                if (ContractorContactID > 0 || ContractorId > 0)
                {
                    var getContractorContactTxn = await _contractorService.GetContractorContactPerson(ContractorContactID, ContractorId);
                    if (getContractorContactTxn == null)
                        ContractorContactTxn.ContractorID = ContractorId;
                    else
                        ContractorContactTxn = getContractorContactTxn;

                    return PartialView("ContactPerson/_AddUpdateContractorContactPerson", ContractorContactTxn);
                }
                else
                    return PartialView("ContactPerson/_AddUpdateContractorContactPerson", ContractorContactTxn);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddUpdateContractorContactPerson(ContractorContactTxn contractorContactTxn)
        {
            try
            {
                int ContractorContactId = await _contractorService.AddUpdateContractorContactPerson(contractorContactTxn);
                if (ContractorContactId > 0)
                {
                    //return RedirectToAction("AddUpdateContractor", new { id = contractorContactTxn.ContractorID });
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

        #endregion Contractor Contact Person List

        #region Contractor Address

        [HttpGet]
        public async Task<PartialViewResult> AddUpdateContractorAddress(int ContractorAddressTxnID, int ContractorId)
        {
            try
            {
                LeadAddressTxn ContractorAddressTxn = new LeadAddressTxn();
                ContractorAddressTxn.ContractorID = ContractorId;
                if (ContractorAddressTxnID > 0 || ContractorId > 0)
                {
                    var getContractorAddressTxn = await _contractorService.GetContractorAddressTxn(ContractorAddressTxnID, ContractorId);
                    if (getContractorAddressTxn == null)
                        ContractorAddressTxn.ContractorID = ContractorId;
                    else
                        ContractorAddressTxn = getContractorAddressTxn;

                    return PartialView("Address/_addUpdateContractorAddress", ContractorAddressTxn);
                }
                else
                    return PartialView("Address/_addUpdateContractorAddress", ContractorAddressTxn);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddUpdateContractorAddress(LeadAddressTxn contractorAddressTxn)
        {
            contractorAddressTxn.CreatedOrModifiedBy = USERID;
            try
            {
                int ContractorAddressTxnID = await _contractorService.AddUpdateContractorAddress(contractorAddressTxn);
                if (ContractorAddressTxnID > 0)
                {
                    // return RedirectToAction("AddUpdateContractor", new { id = contractorAddressTxn.ContractorID });
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




        /*[HttpPost]
        public JsonResult AddUpdateContractorAddress(string Prefix)
        {
            //Note : you can bind same list from database  
            List<ContractorAddressTxn> ObjList = new List<ContractorAddressTxn>()
            {

                new ContractorAddressTxn {Id=1,Name="Latur" },
                new ContractorAddressTxn {Id=2,Name="Mumbai" },
                new ContractorAddressTxn {Id=3,Name="Pune" },
                new ContractorAddressTxn {Id=4,Name="Delhi" },
                new ContractorAddressTxn {Id=5,Name="Dehradun" },
                new ContractorAddressTxn {Id=6,Name="Noida" },
                new ContractorAddressTxn {Id=7,Name="New Delhi" }

        };
            //Searching records from list using LINQ query  
            var Name = (from N in ObjList where N.Name.StartsWith(Prefix) select new { N.Name });
            return Json(Name, JsonRequestBehavior.AllowGet);
        }*/
        [HttpGet]
        public JsonResult GetAllStateContractor(string search)
        {

            var testSTate = _masterService.GetAllStateAsync(search).Result;
            return Json(testSTate);
        }

        #endregion Contractor Address 

        #region Contractor Contact Details 

        [HttpPost]
        public async Task<ActionResult> AddUpdateContractorContactDetail(LeadContactDetail contractorContactDetail)
        {
            try
            {
                int ContactId = await _contractorService.AddUpdateContractorContactDetail(contractorContactDetail);
                if (ContactId > 0)
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

        #endregion Contractor Contact Details

        #region Contractor Registration

        [HttpPost]
        public async Task<ActionResult> AddUpdateContractorRegistration(LeadRegistration contractorRegistration)
        {
            try
            {
                contractorRegistration.CreatedOrModifiedBy = USERID;
                int registrationId = await _contractorService.AddUpdateContractorRegistration(contractorRegistration);
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

        #endregion Contractor Registration 

        #region Contractor Bank Details

        [HttpGet]
        public async Task<PartialViewResult> AddUpdateContractorBankAccount(int ContractorBankDetailId, int ContractorId)
        {
            try
            {
                LeadBankDetails ContractorBankDetails = new LeadBankDetails();
                ContractorBankDetails.ContractorID = ContractorId;
                if (ContractorBankDetailId > 0 || ContractorId > 0)
                {
                    var ContractorBankdetail = await _contractorService.GetContractorBankAccount(ContractorBankDetailId, ContractorId);
                    if (ContractorBankdetail == null)
                        ContractorBankDetails.ContractorID = ContractorId;
                    else
                        ContractorBankDetails = ContractorBankdetail;

                    return PartialView("BankDetail/_AddUpdateContractorBankAccount", ContractorBankDetails);
                }
                else
                    return PartialView("BankDetail/_AddUpdateContractorBankAccount", ContractorBankDetails);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddUpdateContractorBankAccount(LeadBankDetails ContractorBankDetails)
        {
            try
            {
                ContractorBankDetails.CreatedOrModifiedBy = USERID;
                int ContractorBankAccountId = await _contractorService.AddUpdateContractorBankDetails(ContractorBankDetails);
                if (ContractorBankAccountId > 0)
                {
                    //return RedirectToAction("AddUpdateContractor", new { id = ContractorBankDetails.ContractorID });
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

        #endregion Contractor Bank Details

        #region Contractor Setting
        [HttpPost]
        public async Task<ActionResult> AddUpdateContractorSetting(LeadSetting ContractorSetting)
        {
            try
            {
                int settingId = await _contractorService.AddUpdateContractorSetting(ContractorSetting);
                if (settingId > 0)
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

        #endregion Contractor Bank Setting

        #region Contractor Document
        [HttpGet]
        public async Task<PartialViewResult> AddUpdateContractorDocument(int ContractorDocumentId, int ContractorId)
        {
            try
            {
                LeadDocument ContractorDocument = await _contractorService.GetContractorDocument(ContractorDocumentId, ContractorId);

                if (ContractorDocument == null)
                    ContractorDocument = new LeadDocument { ContractorID = ContractorId, };

                ViewData["DocumentPath"] = ContractorDocument.DocumentPath;

                return PartialView("Document/_AddUpdateContractorDocument", ContractorDocument);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddUpdateContractorDocument(LeadDocument ContractorDocument)
        {
            try
            {
                string EncryID = string.Empty;
                if (ContractorDocument.ContractorID > 0)
                {
                    EncryID = protector.Protect(ContractorDocument.ContractorID.ToString());
                }
                int id = ContractorDocument.ContractorID;
                if (ContractorDocument.DocumentFile != null)
                {
                    var path1 = "";
                    string fileExtension = Path.GetExtension(ContractorDocument.DocumentFile.FileName);

                    path1 = _webHostEnvironment.WebRootPath + linkContractorDocument;  //full path Excluding file name ----  0
                    string filepath = path1;  //full path including file name  -----  1
                    //string filename = ContractorDocument.DocumentName + "_" + ContractorDocument.ContractorID + "_" + DateTime.Now.ToString().Replace("/", "").Replace(" ", "").Replace("-", "").Replace(":", "") + fileExtension;
                    string filename = ContractorDocument.DocumentName.Replace("/", "").Replace(" ", "").Replace("-", "").Replace(":", "") + fileExtension;
                    string dbfilepath = linkContractorDocument + filename;
                    filepath = filepath + filename;

                    if (!Directory.Exists(path1))
                    {
                        Directory.CreateDirectory(path1);
                    }

                    if (Directory.Exists(path1))
                    {
                        using (var fileStream = new FileStream(filepath, FileMode.Create, FileAccess.ReadWrite))
                        {
                            ContractorDocument.DocumentFile.CopyTo(fileStream);
                        }
                        ContractorDocument.DocumentName = filename;
                        ContractorDocument.DocumentExtension = fileExtension;
                        ContractorDocument.DocumentPath = dbfilepath;
                        int documentId = await _contractorService.AddUpdateContractorDocument(ContractorDocument);
                        if (documentId > 0)
                        {
                            //TempData["message"] = MessageHelper.Uploaded;
                            //ViewBag.Message = MessageHelper.Uploaded;
                            //return Json(new { status = true, message = MessageHelper.Uploaded });
                            return RedirectToAction("AddUpdateContractor", new { id = EncryID });
                        }
                        else
                        {
                            //ViewBag.Message = MessageHelper.Uploaded;
                            //return Json(new { status = false, message = MessageHelper.NoDocument });
                            return RedirectToAction("AddUpdateContractor", new { id = EncryID });
                        }
                    }
                }
                //ViewBag.Message = MessageHelper.NoDocument;
                //return Json(new { status = false, message = MessageHelper.Error });
                return RedirectToAction("AddUpdateContractor", new { id = EncryID });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }


        #endregion Contractor Document

        #region Contractor Document Image Download

        /*public ActionResult OpenImage()
        {
            byte[] imageData = ; // Retrieve image data from a file or database
            string contentType = ; // Determine the MIME type of the image (e.g. "image/png")

            return File(imageData, contentType);
        }
        public FileResult DownloadFile(string Name)
        {
            string path = Server.MapPath("~/ProductList/") + Name;
            byte[] bytes = System.IO.File.ReadAllBytes(path);
            return File(bytes, "application/octet-stream", Name);
        }*/

        public FileResult OpenImage(string fileName)
        {
            // byte[] fileBytes = System.IO.File.ReadAllBytes(@"C:/Users/Lenovo/Documents/GitHub/ERP/ERP/wwwroot/assets/images/ContractorDocuments/"+fileName+"");
            byte[] fileBytes = System.IO.File.ReadAllBytes(@_webHostEnvironment.WebRootPath + linkContractorDocument + fileName);
            //string fileName = "12thMarkSheetOG.jpg";
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }
        #endregion Contractor Document Image Download

    }
}
