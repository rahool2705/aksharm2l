using Business.Interface.HR;
using Business.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Business.SQL;
using Microsoft.AspNetCore.Http;
using System;
using GridShared;
using Business.Entities.HR;
using GridCore.Server;
using ERP.Controllers;
using ERP.Helpers;
using System.Threading.Tasks;
using System.ComponentModel;
using Business.Interface.IEmployee;
using PartialViewResult = Microsoft.AspNetCore.Mvc.PartialViewResult;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using ActionResult = Microsoft.AspNetCore.Mvc.ActionResult;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using AuthorizeAttribute = Microsoft.AspNetCore.Authorization.AuthorizeAttribute;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ERP.Areas.HR.Controllers
{
    [Area("HR"), Authorize]
    [DisplayName("HRConfig")]
    public class HRConfigController : SettingsController
    {
        private readonly IConfiguration _configuration;
        private readonly IHRConfigService _hRConfigService;
        private readonly IMasterService _masterService;
        private readonly IEmployeeService _employeeService;

        public HRConfigController(IConfiguration configuration, IHRConfigService hRConfigService, IMasterService masterService, IEmployeeService employeeService)
        {
            _configuration = configuration;
            _hRConfigService = hRConfigService;
            _masterService = masterService;
            _employeeService = employeeService;

        }

        [HttpGet]
        public ActionResult Index([FromQuery(Name = "grid-page")] string gridpage = "1", [FromQuery(Name = "grid-column")] string orderby = "", [FromQuery(Name = "grid-dir")] string sortby = "0", [FromQuery(Name = "grid-filter")] string gridfilter = "", [FromQuery(Name = "grid-search")] string search = "")
        {
            int userid = USERID;
            IQueryCollection query = Request.Query;
            string value = string.Empty;

            Action<IGridColumnCollection<HRConfig>> columns = c =>
            {
                c.Add(o => o.Year).Titled("Year").Sortable(true);
                c.Add(o => o.EmployeeCategoryText).Titled("Emp. Category").Sortable(true);
                c.Add(o => o.WorkingDayInMonth).Titled("No.of Days").Sortable(true);
                c.Add(o => o.WorkingHrsInDay).Titled("Hours in Days").Sortable(true);
                c.Add(o => o.OTPaymentIn).Titled("OT Hours").Sortable(true);
                c.Add(o => o.WeekOff1).Titled("1st Weekoff").Sortable(true);
                c.Add(o => o.WeekOff2).Titled("2nd Weekoff").Sortable(true);
                //Below code hide on phones
                c.Add().Titled("Edit").Encoded(false).Sanitized(false).SetWidth(60).Css("hidden-xs")
                .RenderValueAs(o => $"<a class='btn' onclick='fnHRConfig(this)' href='javascript:void(0)' data-id='{o.HRConfigID}' data-bs-toggle='offcanvas' data-bs-target='#canvasHRConfig' aria-controls='canvasHRConfig'><i class='bx bx-edit'></i> </a>");
            };
            PagedDataTable<HRConfig> pds = _hRConfigService.GetAllHRConfigAsync(gridpage.ToInt(), PAGESIZE, search, orderby.RemoveSpace(), sortby == "0" ? "ASC" : "DESC").Result;
            var server = new GridCoreServer<HRConfig>(pds, query, false, "ordersGrid",
                columns, PAGESIZE, pds.TotalItemCount)
                .Sortable()
                .ClearFiltersButton(true)
                .Selectable(true)
                .WithGridItemsCount()
                .ChangeSkip(false)
                .EmptyText("No record found")
                .ClearFiltersButton(false);
            return View(server.Grid);
        }

        #region HR Config

        [HttpGet]
        public async Task<PartialViewResult> AddUpdateHRConfig(int hRConfigID)
        {
          /*  var employeeList = _employeeService.GetAllEmployeesAsync().Result;
            ViewData["EmployeeList"] = new SelectList(employeeList, "EmployeeID", "EmployeeName");*/
            try
            {
                HRConfig hRConfig = await _hRConfigService.GetHRConfigAsync(hRConfigID);

                if (hRConfig == null)
                    hRConfig = new HRConfig { HRConfigID = hRConfigID };

                return PartialView("_addUpdateHRConfig", hRConfig);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddUpdateHRConfig(HRConfig HRConfig)
        {
            try
            {
                HRConfig.CreatedOrModifiedBy = USERID;
                int HRConfigID = await _hRConfigService.HRConfigCreateOrUpdateAsync(HRConfig);
                if (HRConfigID > 0)
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

        #endregion HR Config

    }
}
