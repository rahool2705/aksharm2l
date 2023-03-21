using Business.Entities.Customer;
using Business.Entities.Employee;
using Business.Interface;
using Business.Interface.ICustomer;
using Business.SQL;
using ERP.Controllers;
using ERP.Helpers;
using GridCore.Server;
using GridShared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ERP.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize]
    public class CustomerController : SettingsController
    {
        private readonly IMasterService _masterService;
        private readonly ICustomerService _customerService;
        private readonly IConfiguration _configuration;
        private readonly IHostEnvironment _hostEnvironment;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string linkCustomerLogoImage;
        private readonly string linkCustomerDocument;
        public CustomerController(IMasterService masterService, IConfiguration configuration, IHostEnvironment hostEnvironment, IWebHostEnvironment webHostEnvironment, ICustomerService customerService)
        {
            _masterService = masterService;
            _configuration = configuration;
            _hostEnvironment = hostEnvironment;
            _webHostEnvironment = webHostEnvironment;
            _customerService = customerService;
            linkCustomerLogoImage = _configuration.GetSection("CustomerLogoImagePath")["CustomerLogoImage"];
            linkCustomerDocument = _configuration.GetSection("CustomerLogoImagePath")["CustomerDocuments"];
        }
        public ActionResult Index([FromQuery(Name = "grid-page")] string gridpage = "1", [FromQuery(Name = "grid-column")] string orderby = "", [FromQuery(Name = "grid-dir")] string sortby = "0", [FromQuery(Name = "grid-filter")] string gridfilter = "", [FromQuery(Name = "grid-search")] string search = "")
        {
            int userid = USERID;
            IQueryCollection query = Request.Query;
            string value = string.Empty;
            Action<IGridColumnCollection<CustomerMaster>> columns = c =>
            {
                c.Add(o => o.SrNo, "SrNo").Titled("Sr.No.").SetWidth(20);
                c.Add(o => o.CustomerCode).Titled("Customer Code").Sortable(true);
                c.Add(o => o.CustomerName).Titled("Customer Name").Sortable(true);
                c.Add(o => o.GroupName).Titled("Group Name").Sortable(true);
                c.Add(o => o.OwnerName).Titled("Owner Name").Sortable(true);
                c.Add(o => o.IsActive).Titled("IsActive").Sortable(true);
                //Below code hide on phones
                c.Add().Titled("Details").Encoded(false).Sanitized(false).SetWidth(60).Css("hidden-xs")
                .RenderValueAs(o => $"<a class='btn IndexPagebtnEidtPadding' href='/Admin/Customer/AddUpdateCustomer/{o.CustomerID}' ><i class='bx bx-edit'></i></a>");
            };
            PagedDataTable<CustomerMaster> pds = _customerService.GetAllCustomerAsync(gridpage.ToInt(), PAGESIZE, search, orderby.RemoveSpace(), sortby == "0" ? "ASC" : "DESC").Result;
            var server = new GridCoreServer<CustomerMaster>(pds, query, false, "ordersGrid",
                columns, PAGESIZE, pds.TotalItemCount)
                .Sortable().ClearFiltersButton(true).Selectable(true).WithGridItemsCount().ChangeSkip(false).EmptyText("No record found").ClearFiltersButton(false);
            return View(server.Grid);
        }

        #region Basic details
        [HttpGet]
        public ActionResult AddUpdateCustomer(int id)
        {
            try
            {
                CustomerMaster customerMaster = new CustomerMaster();
                if (id > 0)
                {
                    customerMaster = _customerService.GetCustomerAsync(id).Result;
                    ViewData["LogoImage"] = customerMaster.LogoImagePath;
                }
                return View("AddUpdateCustomer", customerMaster);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddUpdateCustomer(CustomerMaster customerMaster)
        {

            var path1 = "";
            var id = await _customerService.AddUpdateCustomer(customerMaster);

            if (id > 0)
            {
                if (customerMaster.LogoImage != null)
                {
                    string fileExtension = Path.GetExtension(customerMaster.LogoImage.FileName);
                    //Add logic for save file in image folder. 29-09-2022.
                    CustomerLogoImage customerLogoImage = new CustomerLogoImage();
                    path1 = _webHostEnvironment.WebRootPath + linkCustomerLogoImage;  //full path Excluding file name ----  0
                    string filepath = path1;  //full path including file name  -----  1
                    string filename = id + "_" + DateTime.Now.ToString().Replace("/", "").Replace(" ", "").Replace("-", "").Replace(":", "") + "_" + customerMaster.LogoImage.FileName;
                    string dbfilepath = linkCustomerLogoImage + filename;
                    filepath = filepath + filename;
                    customerLogoImage.CustomerID = id;
                    customerLogoImage.LogoImageName = filename;
                    customerLogoImage.LogoImagePath = dbfilepath;
                    customerLogoImage.CreatedOrModifiedBy = USERID;
                    customerLogoImage.IsActive = true;
                    if (Directory.Exists(path1))
                    {
                        using (var fileStream = new FileStream(filepath, FileMode.Create, FileAccess.ReadWrite))
                        {
                            customerMaster.LogoImage.CopyTo(fileStream);
                        }
                        var profilePhotoId = _customerService.UpdateCustomerLogoImage(customerLogoImage).Result;
                        //return Json(new { status = false, message = MessageHelper.Error });
                        return RedirectToAction("AddUpdateCustomer", new { id = id });
                    }
                }
                //return Json(new { status = true, message = MessageHelper.Added });
                return RedirectToAction("AddUpdateCustomer", new { id = id });

            }
            else
            {
                //return Json(new { status = false, message = MessageHelper.Error });
                return RedirectToAction("AddUpdateCustomer");
            }
        }
        #endregion Basic details

        #region Contact Person Detail
        [HttpGet]
        public async Task<PartialViewResult> AddUpdateCustomerContactPerson(int customerContactID, int customerId)
        {
            try
            {
                CustomerContactTxn customerContactTxn = new CustomerContactTxn();
                customerContactTxn.CustomerID = customerId;
                if (customerContactID > 0 || customerId > 0)
                {
                    var getCustomerContactTxn = await _customerService.GetCustomerContactPerson(customerContactID, customerId);
                    if (getCustomerContactTxn == null)
                        customerContactTxn.CustomerID = customerId;
                    else
                        customerContactTxn = getCustomerContactTxn;

                    return PartialView("_addUpdateCustomerContactPerson", customerContactTxn);
                }
                else
                    return PartialView("_addUpdateCustomerContactPerson", customerContactTxn);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddUpdateCustomerContactPerson(CustomerContactTxn customerContactTxn)
        {
            try
            {
                customerContactTxn.CreatedOrModifiedBy = USERID;
                int customerContactId = await _customerService.AddUpdateCustomerContactPerson(customerContactTxn);
                if (customerContactId > 0)
                {
                    //return RedirectToAction("AddUpdateCustomer", new { id = customerContactTxn.CustomerID });
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
        #endregion Contact Person Detail

        #region Address details
        [HttpGet]
        public async Task<PartialViewResult> AddUpdateCustomerAddress(int customerAddressTxnId, int customerId)
        {
            try
            {
                CustomerAddressTxn customerAddressTxn = new CustomerAddressTxn();
                customerAddressTxn.CustomerID = customerId;
                if (customerAddressTxnId > 0 || customerId > 0)
                {
                    var CustomerAddressTxn = await _customerService.GetCustomerAddressTxn(customerAddressTxnId, customerId);
                    if (CustomerAddressTxn == null)
                        customerAddressTxn.CustomerID = customerId;
                    else
                        customerAddressTxn = CustomerAddressTxn;

                    return PartialView("_addUpdateCustomerAddress", customerAddressTxn);
                }
                else
                    return PartialView("_addUpdateCustomerAddress", customerAddressTxn);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddUpdateCustomerAddress(CustomerAddressTxn customerAddressTxn)
        {
            try
            {
                int customerAddressId = await _customerService.AddUpdateCustomeAddress(customerAddressTxn);
                if (customerAddressId > 0)
                {
                    return RedirectToAction("AddUpdateCustomer", new { id = customerAddressTxn.CustomerID });
                    //return Json(new { status = true, message = MessageHelper.Added });
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

        #region Customer Contact Detail
        [HttpPost]
        public async Task<ActionResult> AddUpdateCustomerContactDetail(CustomerContactDetail customerContactDetail)
        {
            try
            {
                int ContactId = await _customerService.AddUpdateCustomerContactDetail(customerContactDetail);
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

        #endregion Customer Contact Detail

        #region Customer Registration
        [HttpPost]
        public async Task<ActionResult> AddUpdateCustomerRegistration(CustomerRegistration customerRegistration)
        {
            try
            {
                customerRegistration.CreatedOrModifiedBy = USERID;
                int registrationId = await _customerService.AddUpdateCustomerRegistration(customerRegistration);
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

        #endregion Customer Registration

        #region Customer Bank Details
        [HttpGet]
        public async Task<PartialViewResult> AddUpdateCustomerBankAccount(int CustomerBankDetailId, int customerId)
        {
            try
            {
                CustomerBankDetails customerBankDetails = new CustomerBankDetails();
                customerBankDetails.CustomerID = customerId;
                if (CustomerBankDetailId > 0 || customerId > 0)
                {
                    var customerBankdetail = await _customerService.GetCustomerBankAccount(CustomerBankDetailId, customerId);
                    if (customerBankdetail == null)
                        customerBankDetails.CustomerID = customerId;
                    else
                        customerBankDetails = customerBankdetail;

                    return PartialView("_addUpdateCustomerBankAccount", customerBankDetails);
                }
                else
                    return PartialView("_addUpdateCustomerBankAccount", customerBankDetails);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddUpdateCustomerBankAccount(CustomerBankDetails customerBankDetails)
        {
            try
            {
                customerBankDetails.CreatedOrModifiedBy = USERID;
                int customerBankAccountId = await _customerService.AddUpdateCustomerBankDetails(customerBankDetails);
                if (customerBankAccountId > 0)
                {
                    //return RedirectToAction("AddUpdateCustomer", new { id = customerBankDetails.CustomerID });
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
        #endregion Customer Bank Details

        #region Customer document
        [HttpGet]
        public async Task<PartialViewResult> AddUpdateCustomerDocument(int customerDocumentId, int customerId)
        {
            try
            {
                CustomerDocument customerDocument = await _customerService.GetCustomerDocument(customerDocumentId, customerId);

                if (customerDocument == null)
                    customerDocument = new CustomerDocument { CustomerID = customerId };

                ViewData["DocumentPath"] = customerDocument.DocumentPath;

                return PartialView("_addUpdateCustomerDocument", customerDocument);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddUpdateCustomerDocument(CustomerDocument customerDocument)
        {
            try
            {
                int id = customerDocument.CustomerID;
                if (customerDocument.DocumentFile != null)
                {
                    var path1 = "";
                    string fileExtension = Path.GetExtension(customerDocument.DocumentFile.FileName);

                    path1 = _webHostEnvironment.WebRootPath + linkCustomerDocument;  //full path Excluding file name ----  0
                    string filepath = path1;  //full path including file name  -----  1
                    string filename = customerDocument.DocumentName + "_" + customerDocument.CustomerID + "_" + DateTime.Now.ToString().Replace("/", "").Replace(" ", "").Replace("-", "").Replace(":", "") + fileExtension;
                    string dbfilepath = linkCustomerDocument + filename;
                    filepath = filepath + filename;

                    if (Directory.Exists(path1))
                    {
                        using (var fileStream = new FileStream(filepath, FileMode.Create, FileAccess.ReadWrite))
                        {
                            customerDocument.DocumentFile.CopyTo(fileStream);
                        }
                        customerDocument.DocumentName = filename;
                        customerDocument.DocumentExtension = fileExtension;
                        customerDocument.DocumentPath = dbfilepath;
                        int documentId = await _customerService.AddUpdateCustomerDocument(customerDocument);
                        if (documentId > 0)
                        {
                            //TempData["message"] = MessageHelper.Uploaded;
                            //ViewBag.Message = MessageHelper.Uploaded;
                            //return Json(new { status = true, message = MessageHelper.Uploaded });
                            return RedirectToAction("AddUpdateCustomer", new { id = id });
                        }
                        else
                        {
                            //ViewBag.Message = MessageHelper.Uploaded;
                            //return Json(new { status = false, message = MessageHelper.NoDocument });
                            return RedirectToAction("AddUpdateCustomer", new { id = id });
                        }
                    }
                }
                //ViewBag.Message = MessageHelper.NoDocument;
                //return Json(new { status = false, message = MessageHelper.Error });
                return RedirectToAction("Index", "Customer");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult> ActiveInActiveCustomerDocument(int customerDocumentId, int customerId, bool isActive)
        {
            CustomerDocument customerDocument = new CustomerDocument()
            {
                CustomerDocumentID = customerDocumentId,
                CustomerID = customerId,
                IsActive = isActive,
                CreatedOrModifiedBy = USERID
            };
            int modifiedBy = USERID;
            int customerDocumentIsActive = await _customerService.ActiveInActiveCustomerDocument(customerDocument);
            if (customerDocumentIsActive > 0)
            {
                if (isActive)
                    return Json(new { status = true, message = MessageHelper.ActivatedDocument });
                else
                    return Json(new { status = true, message = MessageHelper.InactivatedDocument });
            }
            else
                return Json(new { status = true, message = MessageHelper.Error });
        }
        #endregion Customer document

        #region Customer Setting
        [HttpPost]
        public async Task<ActionResult> AddUpdateCustomerSetting(CustomerSetting customerSetting)
        {
            try
            {
                int settingId = await _customerService.AddUpdateCustomerSetting(customerSetting);
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
        #endregion Customer Setting
    }
}
