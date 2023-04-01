using Business.Entities.PositiveNoteModel;
using Business.Interface.IPositiveNoteService;
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
using HttpPostAttribute = System.Web.Mvc.HttpPostAttribute;
using PartialViewResult = Microsoft.AspNetCore.Mvc.PartialViewResult;

namespace ERP.Areas.SuperAdmin.Controllers
{
    [Area("SuperAdmin"), Authorize]
    [DisplayName("PositiveNote")]
    public class PositiveNoteController : SettingsController
    {
        private readonly IPositiveNoteService iPositiveNote;
        public PositiveNoteController(IPositiveNoteService iPositiveNote)
        {
            this.iPositiveNote = iPositiveNote;
        }

        #region Index Page
        public IActionResult Index([FromQuery(Name = "grid-page")] string gridpage = "1", [FromQuery(Name = "grid-column")] string orderby = "", [FromQuery(Name = "grid-dir")] string sortby = "0", [FromQuery(Name = "grid-filter")] string gridfilter = "", [FromQuery(Name = "grid-search")] string search = "")
        {
            int userid = USERID;
            IQueryCollection query = Request.Query;
            string value = string.Empty;
            Action<IGridColumnCollection<PositiveNote>> columns = c =>
            {
                c.Add(o => o.SrNo)
                    .Titled("SrNo")
                    .SetWidth(10);

                c.Add(o => o.PositiveNoteText)
                    .Titled("Feedback Notes")
                    .SetWidth(70);

                /*c.Add(o => o.Remark)
                    .Titled("Remark")
                    .SetWidth(160);*/

                c.Add()
                    .Titled("Edit")
                    .Encoded(false)
                    .Sanitized(false)
                    .SetWidth(60)
                    .Css("hidden-xs") //hide on phones
                    .RenderValueAs(o => $"<a class='btn IndexPagebtnEidtPadding' onclick='fnPositiveNote(this)' href='javascript:void(0)' data-id='{o.PositiveNoteID}' data-bs-toggle='offcanvas' data-bs-target='#canvas_PositiveNote' aria-controls='canvas_masterentity' ><i class='bx bx-edit'></i></a>");
            };
            PagedDataTable<PositiveNote> pds =(PagedDataTable<PositiveNote>) iPositiveNote.GetAllPositiveNoteAsync(gridpage.ToInt(), PAGESIZE, search, orderby.RemoveSpace(), sortby == "0" ? "ASC" : "DESC").Result;

            var server = new GridCoreServer<PositiveNote>(pds, query, false, "ordersGrid", columns, PAGESIZE, pds.TotalItemCount)
                .Sortable()
                .Searchable(false, false)
                .Selectable(true)
                .WithGridItemsCount()
                .ChangeSkip(false)
                .EmptyText("No record found")
                .ClearFiltersButton(false)
                .WithPaging(PAGESIZE, pds.TotalItemCount, PAGESIZE, "grid-page");


            return View(server.Grid);
        }

        #endregion Index Page

        #region Slider Page

        [HttpPost]
        public PartialViewResult Get(int id, int key)
        {
            try
            {
                PositiveNote model = new PositiveNote();
                model.PositiveNoteID = id;
                if (Convert.ToInt32(id) > 0)
                {
                    model = iPositiveNote.GetPositiveNoteAsync(id).Result;

                    return PartialView("AddOrUpdatePositiveNote", model);
                }
                return PartialView("AddOrUpdatePositiveNote", model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        #endregion Slider Page

        #region Add or Update Positive Note

        [HttpPost]
        public async Task<IActionResult> AddOrUpdatePositiveNote(PositiveNote model)
        {
            model.CreatedOrModifiedBy = USERID;
            var _PositiveNoteID = await iPositiveNote.AddOrUpdatePositiveNote(model);

            if (_PositiveNoteID > 0)
            {
                model.PositiveNoteID = _PositiveNoteID;
                return Json(new { status = true, message = MessageHelper.Added });
            }
            else
                return Json(new { status = false, message = MessageHelper.Error });
            // If we got this far, something failed, redisplay form
        }

        #endregion Add or Update Positive Note
    }
}
