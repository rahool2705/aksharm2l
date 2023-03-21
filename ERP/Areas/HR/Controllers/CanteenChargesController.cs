using Business.Entities.CanteenCharges;
using Business.Interface;
using Business.Interface.CanteenCharges;
using Business.SQL;
using ERP.Controllers;
using ERP.Helpers;
using GridCore.Server;
using GridShared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace ERP.Areas.HR.Controllers
{
    [Area("HR"), Authorize]
    [DisplayName("CanteenCharges")]
    public class CanteenChargesController : SettingsController
    {
        private readonly ICanteenChargesService _canteenChargesService;
        private readonly IMasterService _masterService;


        public CanteenChargesController(ICanteenChargesService canteenChargesService, IMasterService masterService)
        {
            _canteenChargesService = canteenChargesService;
            _masterService = masterService;
        }
        [HttpGet]
        public ActionResult Index([FromQuery(Name = "grid-page")] string gridpage = "1", [FromQuery(Name = "grid-column")] string orderby = "", [FromQuery(Name = "grid-dir")] string sortby = "0", [FromQuery(Name = "grid-filter")] string gridfilter = "", [FromQuery(Name = "grid-search")] string search = "")
        {
            int userid = USERID;
            IQueryCollection query = Request.Query;
            string value = string.Empty;

            Action<IGridColumnCollection<CanteenCharges>> columns = c =>
            {
                c.Add(o => o.SrNo).Titled("Sr No.").Sortable(true);
                c.Add(o => o.EmployeeName).Titled("Employee Name").Sortable(true);
                c.Add(o => o.Month).Titled("Month").Sortable(true);
                c.Add(o => o.Year).Titled("Year").Sortable(true);
                c.Add(o => o.BreakfastQuantity).Titled("Breakfast Quantity").Sortable(true);
                c.Add(o => o.BreakfastRate).Titled("Breakfast Rate").Sortable(true);
                c.Add(o => o.LunchQuantity).Titled("Lunch Quantity").Sortable(true);
                c.Add(o => o.LunchRate).Titled("Lunch Rate").Sortable(true);
                c.Add(o => o.DinnerQuantity).Titled("Dinner Quantity").Sortable(true);
                c.Add(o => o.DinnerRate).Titled("Dinner Rate").Sortable(true);

                //c.Add(o => o.TotalWagesPerDay).Titled("Total Wages Per Day").Sortable(true);
                //c.Add(o => o.IsActive).Titled("Status").Sortable(true);
                //c.Add(o => o.StartDate).Titled("Start Date").Sortable(true).Format("{0:dd/MM/yyyy}");
                //c.Add(o => o.EndDate).Titled("End Date").Sortable(true).Format("{0:dd/MM/yyyy}");
                //c.Add(o => o.EntryDate).Titled("Entry Date").Sortable(true).Format("{0:dd/MM/yyyy}");
                //Below code hide on phones
                c.Add().Titled("Edit").Encoded(false).Sanitized(false).SetWidth(60).Css("hidden-xs")
                .RenderValueAs(o => $"<a class='btn' onclick='fnCanteenCharges(this)' href='javascript:void(0)' data-id='{o.CanteenChargesID}' data-bs-toggle='offcanvas' data-bs-target='#canvasCanteenCharges' aria-controls='canvasCanteenCharges'><i class='bx bx-edit'></i> </a>");
            };
            PagedDataTable<CanteenCharges> pds = _canteenChargesService.GetAllCanteenChargesAsync(gridpage.ToInt(), PAGESIZE, search, orderby.RemoveSpace(), sortby == "0" ? "ASC" : "DESC").Result;
            var server = new GridCoreServer<CanteenCharges>(pds, query, false, "ordersGrid",
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

        #region Canteen Charges

        [HttpGet]
        public async Task<PartialViewResult> AddUpdateCanteenCharges(int canteenChargesID)
        {
            try
            {
                CanteenCharges canteenCharges = await _canteenChargesService.GetCanteenChargesAsync(canteenChargesID);

                if (canteenCharges == null)
                    canteenCharges = new CanteenCharges { CanteenChargesID = canteenChargesID };

                var listEmployees = _masterService.GetAllEmployees();
                ViewData["EmployeeName"] = new SelectList(listEmployees, "EmployeeID", "EmployeeName");

                return PartialView("_addUpdateCanteenCharges", canteenCharges);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddUpdateCanteenCharges(CanteenCharges canteenCharges)
        {
            try
            {
                canteenCharges.CreatedOrModifiedBy = USERID;
                int canteenChargesID = await _canteenChargesService.CanteenChargesCreateOrUpdateAsync(canteenCharges);
                if (canteenChargesID > 0)
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

        #endregion Canteen Charges
    }
}
