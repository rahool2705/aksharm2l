using Business.Entities.WorkingDaysModel;
using Business.Interface.IWorkingDaysService;
using Business.SQL;
using ERP.Controllers;
using ERP.Helpers;
using GridCore.Server;
using GridShared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Web.Mvc;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using PartialViewResult = Microsoft.AspNetCore.Mvc.PartialViewResult;

namespace ERP.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize]
    [DisplayName("WorkingDays")]
    public class WorkingDaysController : SettingsController
    {
        #region Interface Links
        private readonly IWorkingDaysService iWorkingDays;
        public WorkingDaysController(IWorkingDaysService iWorkingDays)
        {
            this.iWorkingDays = iWorkingDays;
        }
        #endregion Interface Links

        #region Index Page
        public IActionResult Index([FromQuery(Name = "grid-page")] string gridpage = "1", [FromQuery(Name = "grid-column")] string orderby = "", [FromQuery(Name = "grid-dir")] string sortby = "0", [FromQuery(Name = "grid-filter")] string gridfilter = "", [FromQuery(Name = "grid-search")] string search = "")
        {
            int userid = USERID;
            IQueryCollection query = Request.Query;

            Action<IGridColumnCollection<WorkingDay>> columns = c =>
            {
                c.Add(o => o.SrNo)
                    .Titled("SrNo")
                    .SetWidth(10);

                c.Add(o => o.Year)
                    .Titled("Year")
                    .SetWidth(70);

                c.Add(o => o.Month)
                    .Titled("Month")
                    .SetWidth(160);                

                c.Add(o => o.WorkingDays)
                    .Titled("Working Days")
                    .SetWidth(160);

                c.Add(o => o.Holidays)
                    .Titled("Holidays")
                    .SetWidth(160);

                c.Add(o => o.IsActive)
                   .Titled("Status")
                   .SetWidth(160);                

                c.Add()
                    .Titled("Edit")
                    .Encoded(false)
                    .Sanitized(false)
                    .SetWidth(60)
                    .Css("hidden-xs") //hide on phones
                    .RenderValueAs(o => $"<a class='btn IndexPagebtnEidtPadding' onclick='fnWorkingDays(this)' href='javascript:void(0)' data-id='{o.WorkCalendarID}' data-bs-toggle='offcanvas' data-bs-target='#canvas_WorkingDays' aria-controls='canvas_masterentity' ><i class='bx bx-edit'></i></a>");
            };
            PagedDataTable<WorkingDay> pds = iWorkingDays.GetAllWorkingDays(gridpage.ToInt(), PAGESIZE, search, orderby.RemoveSpace(), sortby == "0" ? "ASC" : "DESC").Result;
            var server = new GridCoreServer<WorkingDay>(pds, query, false, "ordersGrid", columns, PAGESIZE, pds.TotalItemCount)
                .Sortable()
                //.Filterable()
                //.WithMultipleFilters()
                .Searchable(true, false)
                //.Groupable(true)
                .ClearFiltersButton(true)
                .Selectable(true)
                //.SetStriped(true)
                //.ChangePageSize(true)
                .WithGridItemsCount()
                //.WithPaging(PAGESIZE, pds.TotalItemCount)
                .ChangeSkip(false)
                .EmptyText("No record found")
                .ClearFiltersButton(false);
            return View("Index", server.Grid);
        }
        #endregion Index Page

        #region Slider Page

        [HttpPost]
        public PartialViewResult Get(int id, int key)
        {
            try
            {
                WorkingDay model = new WorkingDay();
                model.WorkCalendarID = id;
                if (Convert.ToInt32(id) > 0)
                {
                    model = iWorkingDays.GetWorkingDays(id).Result;
                    
                    return PartialView("AddOrUpdateWorkingDays", model);
                }
                return PartialView("AddOrUpdateWorkingDays", model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        #endregion Slider Page

        #region Add Or Update

        [HttpPost]
        public async Task<IActionResult> AddOrUpdateWorkingDays(WorkingDay model)
        {
            model.CreatedOrModifiedBy = USERID;
            var _WorkingDaysID = await iWorkingDays.AddOrUpdateWorkingDays(model);

            if (_WorkingDaysID > 0)
            {
                model.WorkCalendarID = _WorkingDaysID;
                return Json(new { status = true, message = MessageHelper.Added });
            }
            else
                return Json(new { status = false, message = MessageHelper.Error });
            // If we got this far, something failed, redisplay form
        }

        #endregion Add Or Update

        
    }
}
