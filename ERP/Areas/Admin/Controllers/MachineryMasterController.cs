using Business.Entities.Machinery.MachineryMaintainance;
using Business.Entities.Machinery.MachineryMaster;
using Business.Interface.IMachineryMasterService;
using Business.Interface.IMachineryService;
using Business.SQL;
using ERP.Controllers;
using GridCore.Server;
using GridShared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Web.Mvc;

namespace ERP.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize]
    [DisplayName("MachineryMaster")]
    public class MachineryMasterController : SettingsController
    {
        private readonly IMachineryMasterService iMachineryMasterService;
        public MachineryMasterController(IMachineryMasterService iMachineryMasterService)
        {
            this.iMachineryMasterService = iMachineryMasterService;
        }

        public IActionResult Index([FromQuery(Name = "grid-page")] string gridpage = "1", [FromQuery(Name = "grid-column")] string orderby = "", [FromQuery(Name = "grid-dir")] string sortby = "0", [FromQuery(Name = "grid-filter")] string gridfilter = "", [FromQuery(Name = "grid-search")] string search = "")
        {
            int userid = USERID;
            IQueryCollection query = Request.Query;

            Action<IGridColumnCollection<MachineryMaster>> columns = c =>
            {
                c.Add(o => o.SrNo)
                    .Titled("SrNo")
                    .SetWidth(10);

                c.Add(o => o.MachineryMastertext)
                    .Titled("Machine / Equipment Name")
                    .SetWidth(50);

                c.Add(o => o.Manufacturer)
                    .Titled("Manufacturer")
                    .SetWidth(50);

                c.Add(o => o.Remarks)
                    .Titled("Discription")
                    .SetWidth(50);

                c.Add(o => o.Location)
                   .Titled("Location")
                   .SetWidth(50);                

                /*c.Add(o => o.SalesDate)
                    .Titled("Sales Date")
                    .SetWidth(10)
                    .Format("{0:dd/MM/yyyy}");*/

                c.Add(o => o.IsActive)
                   .Titled("Active")
                   .SetWidth(5);

                c.Add()
                    .Titled("Edit")
                    .Encoded(false)
                    .Sanitized(false)
                    .SetWidth(60)
                    .Css("hidden-xs") //hide on phones
                    .RenderValueAs(o => $"<a class='btn' onclick='fnMachinery(this)' href='javascript:void(0)' data-id='{o.MachineryMasterID}' data-bs-toggle='offcanvas' data-bs-target='#canvas_Machinery' aria-controls='canvas_masterentity' ><i class='bx bx-edit'></i></a>");





            };
            PagedDataTable<MachineryMaster> pds = iMachineryMasterService.GetAllMachineryMasterAsync(gridpage.ToInt(), PAGESIZE, search, orderby.RemoveSpace(), sortby == "0" ? "ASC" : "DESC").Result;
            var server = new GridCoreServer<MachineryMaster>(pds, query, false, "ordersGrid",
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
    }
}
