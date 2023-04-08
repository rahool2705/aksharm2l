using Business.Entities.VenueTypeModel;
using Business.Interface.IVenueTypeService;
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
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using PartialViewResult = Microsoft.AspNetCore.Mvc.PartialViewResult;

namespace ERP.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize]
    [DisplayName("VenueType")]
    public class VenueTypeController : SettingsController
    {
        #region Interface Calling

        private readonly IVenueTypeService _iVenueTypeService;
        public VenueTypeController(IVenueTypeService iVenueTypeService)
        {
            this._iVenueTypeService = iVenueTypeService;
        }

        #endregion Interface Calling

        #region Index Page
        [HttpGet]
        public IActionResult Index([FromQuery(Name = "grid-page")] string gridpage = "1", [FromQuery(Name = "grid-column")] string orderby = "", [FromQuery(Name = "grid-dir")] string sortby = "0", [FromQuery(Name = "grid-filter")] string gridfilter = "", [FromQuery(Name = "grid-search")] string search = "")
        {
            int userid = USERID;
            IQueryCollection query = Request.Query;

            Action<IGridColumnCollection<VenueType>> columns = c =>
            {
                c.Add(o => o.SrNo)
                    .Titled("SrNo")
                    .SetWidth(10);

                c.Add(o => o.VenueTypeText)
                    .Titled("Venue Type Name")
                    .SetWidth(70);                

                c.Add()
                    .Titled("Edit")
                    .Encoded(false)
                    .Sanitized(false)
                    .SetWidth(60)
                    .Css("hidden-xs") //hide on phones
                    .RenderValueAs(o => $"<a class='btn IndexPagebtnEidtPadding' onclick='fnVenueType(this)' href='javascript:void(0)' data-id='{o.VenueTypeID}' data-bs-toggle='offcanvas' data-bs-target='#canvas_VenueType' aria-controls='canvas_masterentity' ><i class='bx bx-edit'></i></a>");
            };
            PagedDataTable<VenueType> pds = _iVenueTypeService.GetAllVenueType(gridpage.ToInt(), PAGESIZE, search, orderby.RemoveSpace(), sortby == "0" ? "ASC" : "DESC").Result;
            var server = new GridCoreServer<VenueType>(pds, query, false, "ordersGrid", columns, PAGESIZE, pds.TotalItemCount)
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

        #region Slider Calling

        [HttpPost]
        public PartialViewResult Get(int id, int key)
        {
            try
            {
                VenueType model = new VenueType();
                model.VenueTypeID = id;
                if (Convert.ToInt32(id) > 0)
                {
                    model = _iVenueTypeService.GetVenueType(id).Result;                   
                    return PartialView("AddOrUpdateVenueType", model);
                }
                return PartialView("AddOrUpdateVenueType", model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        #endregion Slider Calling

        #region Add Or Update

        [HttpPost]
        public async Task<IActionResult> AddOrUpdateVenueType(VenueType model)
        {
            model.CreatedOrModifiedBy = USERID;
            var _VenueTypeID = await _iVenueTypeService.VenueTypeAddOrUpdate(model);

            if (_VenueTypeID > 0)
            {
                model.VenueTypeID = _VenueTypeID;
                return Json(new { status = true, message = MessageHelper.Added });
            }
            else
                return Json(new { status = false, message = MessageHelper.Error });
        }

        #endregion Add Or Update
    }
}
