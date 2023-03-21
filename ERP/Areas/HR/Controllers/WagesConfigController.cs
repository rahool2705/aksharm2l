using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using Business.SQL;
using ERP.Helpers;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System;
using ERP.Controllers;
using Business.Entities.WagesConfig;
using GridCore.Server;
using GridShared;
using Business.Interface.IWagesConfig;

namespace ERP.Areas.HR.Controllers
{
    [Area("HR"), Authorize]
    [DisplayName("WagesConfig")]
    public class WagesConfigController : SettingsController
    {
        private readonly IWagesConfigService _wagesConfigService;

        public WagesConfigController(IWagesConfigService wagesConfigService)
        {
            _wagesConfigService = wagesConfigService;
        }
        [HttpGet]
        public ActionResult Index([FromQuery(Name = "grid-page")] string gridpage = "1", [FromQuery(Name = "grid-column")] string orderby = "", [FromQuery(Name = "grid-dir")] string sortby = "0", [FromQuery(Name = "grid-filter")] string gridfilter = "", [FromQuery(Name = "grid-search")] string search = "")
        {
            int userid = USERID;
            IQueryCollection query = Request.Query;
            string value = string.Empty;

            Action<IGridColumnCollection<WagesConfig>> columns = c =>
            {
                c.Add(o => o.SrNo).Titled("Sr No.").Sortable(true);
                c.Add(o => o.EmployeeCategoryText).Titled("Employee Category").Sortable(true);
                c.Add(o => o.MinimumWages).Titled("Minimum Wages").Sortable(true);
                c.Add(o => o.SpecialAllowance).Titled("Special Allowance").Sortable(true);
                c.Add(o => o.TotalWagesPerDay).Titled("Total Wages Per Day").Sortable(true);
                //c.Add(o => o.IsActive).Titled("Status").Sortable(true);
                c.Add(o => o.StartDate).Titled("Start Date").Sortable(true).Format("{0:dd/MM/yyyy}");
                c.Add(o => o.EndDate).Titled("End Date").Sortable(true).Format("{0:dd/MM/yyyy}");
                //c.Add(o => o.EntryDate).Titled("Entry Date").Sortable(true).Format("{0:dd/MM/yyyy}");
                //Below code hide on phones
                c.Add().Titled("Edit").Encoded(false).Sanitized(false).SetWidth(60).Css("hidden-xs")
                .RenderValueAs(o => $"<a class='btn' onclick='fnWagesConfig(this)' href='javascript:void(0)' data-id='{o.WagesConfigID}' data-bs-toggle='offcanvas' data-bs-target='#canvasWagesConfig' aria-controls='canvasWagesConfig'><i class='bx bx-edit'></i> </a>");
            };
            PagedDataTable<WagesConfig> pds = _wagesConfigService.GetAllWagesConfigAsync(gridpage.ToInt(), PAGESIZE, search, orderby.RemoveSpace(), sortby == "0" ? "ASC" : "DESC").Result;
            var server = new GridCoreServer<WagesConfig>(pds, query, false, "ordersGrid",
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

        #region Wages Config

        [HttpGet]
        public async Task<PartialViewResult> AddUpdateWagesConfig(int wagesConfigID)
        {
            try
            {
                WagesConfig wagesConfig = await _wagesConfigService.GetWagesConfigAsync(wagesConfigID);

                if (wagesConfig == null)
                    wagesConfig = new WagesConfig { WagesConfigID = wagesConfigID };

                return PartialView("_addUpdateWagesConfig", wagesConfig);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddUpdateWagesConfig(WagesConfig wagesConfig)
        {
            try
            {
                wagesConfig.CreatedOrModifiedBy = USERID;
                int wagesConfigID = await _wagesConfigService.WagesConfigCreateOrUpdateAsync(wagesConfig);
                if (wagesConfigID > 0)
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

        #endregion Wages Config
    }
}
