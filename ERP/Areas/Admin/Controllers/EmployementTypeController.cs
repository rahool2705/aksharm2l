using Business.Entities.Master.EmployementType;
using Business.Entities.Master.EmployementType;
using Business.Interface.IMaster.IEmployementType;
using Business.Interface.IMaster.IEmployementType;
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
    [DisplayName("EmployementType")]
    public class EmployementTypeController : SettingsController
    {
        private readonly IEmployementTypeService iEmployementTypeService;
        public EmployementTypeController (IEmployementTypeService iEmployementTypeService)
        {
            this.iEmployementTypeService = iEmployementTypeService;
        }
        public IActionResult Index([FromQuery(Name = "grid-page")] string gridpage = "1", [FromQuery(Name = "grid-column")] string orderby = "", [FromQuery(Name = "grid-dir")] string sortby = "0", [FromQuery(Name = "grid-filter")] string gridfilter = "", [FromQuery(Name = "grid-search")] string search = "")
        {
            int userid = USERID;
            IQueryCollection query = Request.Query;

            Action<IGridColumnCollection<EmploymentType>> columns = c =>
            {
                c.Add(o => o.SrNo)
                    .Titled("SrNo")
                    .SetWidth(10);

                c.Add(o => o.EmploymentTypeText)
                    .Titled("Employment Type Name")
                    .SetWidth(50);

                c.Add(o => o.IsActive)
                   .Titled("Active")
                   .SetWidth(5);

                c.Add()
                    .Titled("Edit")
                    .Encoded(false)
                    .Sanitized(false)
                    .SetWidth(60)
                    .Css("hidden-xs") //hide on phones
                    .RenderValueAs(o => $"<a class='btn' onclick='fnEmployementType(this)' href='javascript:void(0)' data-id='{o.EmploymentTypeID}' data-bs-toggle='offcanvas' data-bs-target='#canvas_EmployementType' aria-controls='canvas_EmployementTypeEntity' ><i class='bx bx-edit'></i></a>");





            };
            PagedDataTable<EmploymentType> pds = iEmployementTypeService.GetAllEmployementTypeAsync(gridpage.ToInt(), PAGESIZE, search, orderby.RemoveSpace(), sortby == "0" ? "ASC" : "DESC").Result;
            var server = new GridCoreServer<EmploymentType>(pds, query, false, "ordersGrid",
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
            int userid = USERID;
            try
            {
                EmploymentType model = new EmploymentType();
                model.EmploymentTypeID = id;
                if (Convert.ToInt32(id) > 0)
                {
                    model = iEmployementTypeService.GetEmployementTypeAsync(id).Result;

                    return PartialView("CreateOrUpdateEmployementType", model);
                }
                return PartialView("CreateOrUpdateEmployementType", model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> InsertOrUpdateEmployementType(EmploymentType employementType)
        {
            employementType.CreatedOrModifiedBy = USERID;

            var _employementTypeID = await iEmployementTypeService.InsertOrUpdateEmployementTypeAsync(employementType);

            if (_employementTypeID > 0)
            {
                employementType.EmploymentTypeID = _employementTypeID;
                return Json(new { status = true, message = MessageHelper.Added });
            }
            else
            {
                return Json(new { status = false, message = MessageHelper.Error });
            }
        }

    }
}
