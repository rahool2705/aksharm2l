using Microsoft.AspNetCore.Mvc;
using Business.SQL;
using ERP.Controllers;
using GridCore.Server;
using GridShared;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel;
using System.Web.Mvc;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using PartialViewResult = Microsoft.AspNetCore.Mvc.PartialViewResult;
using Business.Interface.IMachineryService;
using Business.Entities.PartyType;
using ERP.Helpers;
using System.Threading.Tasks;
using Business.Entities.Machinery.MachineryMaintainance;

namespace ERP.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize]
    [DisplayName("MachineryMaintainance")]
    public class MachineryMaintainanceController : SettingsController
    {

        private readonly IMachineryMaintainanceService iMachineryMaintainanceService;
        public MachineryMaintainanceController(IMachineryMaintainanceService iMachineryMaintainanceService)
        {
            this.iMachineryMaintainanceService = iMachineryMaintainanceService;
        }

        public IActionResult Index([FromQuery(Name = "grid-page")] string gridpage = "1", [FromQuery(Name = "grid-column")] string orderby = "", [FromQuery(Name = "grid-dir")] string sortby = "0", [FromQuery(Name = "grid-filter")] string gridfilter = "", [FromQuery(Name = "grid-search")] string search = "")
        {
            int userid = USERID;
            IQueryCollection query = Request.Query;

            Action<IGridColumnCollection<MachineryMaintainance>> columns = c =>
            {
                c.Add(o => o.SrNo)
                    .Titled("SrNo")
                    .SetWidth(10);

                c.Add(o => o.MachineryText)
                    .Titled("Machine / Equipment Name")
                    .SetWidth(50);

                c.Add(o => o.Date)
                    .Titled("Maintainance Date")
                    .SetWidth(10)
                    .Format("{0:dd/MM/yyyy}");

                c.Add(o => o.Note)
                    .Titled("Issue Description")
                    .SetWidth(160);

                c.Add(o => o.Charges)
                    .Titled("Maintainance Charge")
                    .SetWidth(10);

                c.Add(o => o.IsActive)
                   .Titled("Active")
                   .SetWidth(5);

                c.Add()
                    .Titled("Edit")
                    .Encoded(false)
                    .Sanitized(false)
                    .SetWidth(60)
                    .Css("hidden-xs") //hide on phones
                    .RenderValueAs(o => $"<a class='btn' onclick='fnMachinery(this)' href='javascript:void(0)' data-id='{o.MachineryMaintainanceID}' data-bs-toggle='offcanvas' data-bs-target='#canvas_Machinery' aria-controls='canvas_masterentity' ><i class='bx bx-edit'></i></a>");





            };
            PagedDataTable<MachineryMaintainance> pds = iMachineryMaintainanceService.GetAllMachineryMaintainanceAsync(gridpage.ToInt(), PAGESIZE, search, orderby.RemoveSpace(), sortby == "0" ? "ASC" : "DESC").Result;
            var server = new GridCoreServer<MachineryMaintainance>(pds, query, false, "ordersGrid",
                columns, PAGESIZE, pds.TotalItemCount)
                .Sortable()
                .Searchable(true, false)
                .ClearFiltersButton(true)
                .Selectable(true)
                .WithGridItemsCount()
                .ChangeSkip(false)
                .EmptyText("No record found")
                .ClearFiltersButton(false);
            //.WithPaging(PAGESIZE, pds.TotalItemCount)
            //.SetStriped(true)
            //.ChangePageSize(true)
            //.Groupable(true)
            //.Filterable()
            //.WithMultipleFilters();
            return View("Index", server.Grid);
        }

        [HttpPost]
        public PartialViewResult Get(int id, int key)
        {
            try
            {
                MachineryMaintainance model = new MachineryMaintainance();
                model.MachineryMaintainanceID = id;
                if (Convert.ToInt32(id) > 0)
                {
                    model = iMachineryMaintainanceService.GetMachineryMaintainanceAsync(id).Result;

                    /*model = new MarketingFeedback() 
                     { 
                      MarketingFeedbackID = key 
                     };*/
                    return PartialView("CreateOrUpdateMachinery", model);
                }
                return PartialView("CreateOrUpdateMachinery", model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> InsertOrUpdateMachineryMaintainance(MachineryMaintainance MachineryMaintainance)
        {
            MachineryMaintainance.CreatedOrModifiedBy = USERID;
            DateTime date = MachineryMaintainance.Date;

            var _MachineryID = await iMachineryMaintainanceService.InsertOrUpdateMachineryMaintainanceAsync(MachineryMaintainance);

            if (_MachineryID > 0)
            {
                MachineryMaintainance.MachineryMaintainanceID = _MachineryID;
                return Json(new { status = true, message = MessageHelper.Added });
            }
            else
            { 
                return Json(new { status = false, message = MessageHelper.Error }); 
            }

            // If we got this far, something failed, redisplay form
        }

    }
}
