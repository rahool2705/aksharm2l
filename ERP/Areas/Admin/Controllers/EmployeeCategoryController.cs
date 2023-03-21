using Business.Entities.Master.EmployeeCategory;
using Business.Interface.IMaster.IEmployeeCategory;
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
    [DisplayName("EmployeeCategory")]
    public class EmployeeCategoryController : SettingsController
    {
        private readonly IEmployeeCategoryService iEmployeeCategoryService;
        public EmployeeCategoryController(IEmployeeCategoryService iEmployeeCategoryService)
        {
            this.iEmployeeCategoryService = iEmployeeCategoryService;
        }
        public IActionResult Index([FromQuery(Name = "grid-page")] string gridpage = "1", [FromQuery(Name = "grid-column")] string orderby = "", [FromQuery(Name = "grid-dir")] string sortby = "0", [FromQuery(Name = "grid-filter")] string gridfilter = "", [FromQuery(Name = "grid-search")] string search = "")
        {
            int userid = USERID;
            IQueryCollection query = Request.Query;

            Action<IGridColumnCollection<EmployeeCategory>> columns = c =>
            {
                c.Add(o => o.SrNo)
                    .Titled("SrNo")
                    .SetWidth(10);

                c.Add(o => o.EmployeeCategoryText)
                    .Titled("Employee Category Name")
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
                    .RenderValueAs(o => $"<a class='btn' onclick='fnEmployeeCategory(this)' href='javascript:void(0)' data-id='{o.EmployeeCategoryID}' data-bs-toggle='offcanvas' data-bs-target='#canvas_EmployeeCategory' aria-controls='canvas_EmployeeCategoryEntity' ><i class='bx bx-edit'></i></a>");





            };
            PagedDataTable<EmployeeCategory> pds = iEmployeeCategoryService.GetAllEmployeeCategoryAsync(gridpage.ToInt(), PAGESIZE, search, orderby.RemoveSpace(), sortby == "0" ? "ASC" : "DESC").Result;
            var server = new GridCoreServer<EmployeeCategory>(pds, query, false, "ordersGrid",
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
                EmployeeCategory model = new EmployeeCategory();
                model.EmployeeCategoryID = id;
                if (Convert.ToInt32(id) > 0)
                {
                    model = iEmployeeCategoryService.GetEmployeeCategoryAsync(id).Result;
                    
                    return PartialView("CreateOrUpdateEmployeeCategory", model);
                }
                return PartialView("CreateOrUpdateEmployeeCategory", model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> InsertOrUpdateEmployeeCategory(EmployeeCategory employeeCategory)
        {
            employeeCategory.CreatedOrModifiedBy = USERID;
            /*DateTime date = EmployeeCategory.Date;*/

            var _employeeCategoryID = await iEmployeeCategoryService.InsertOrUpdateEmployeeCategoryAsync(employeeCategory);

            if (_employeeCategoryID > 0)
            {
                employeeCategory.EmployeeCategoryID = _employeeCategoryID;
                return Json(new { status = true, message = MessageHelper.Added });
            }
            else
            {
                return Json(new { status = false, message = MessageHelper.Error });
            }
        }
    }
}
