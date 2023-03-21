using Business.Entities;
using Business.Entities.EmployeeLeaveTxn;
using Business.Entities.LeaveTypeMaster;
using Business.Entities.Master.EmployeeCategory;
using Business.Entities.SalaryFormula;
using Business.Interface.ISalaryFormula;
using Business.SQL;
using DocumentFormat.OpenXml.Wordprocessing;
using ERP.Controllers;
using ERP.Extensions;
using ERP.Helpers;
using GridCore.Server;
using GridShared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Areas.HR.Controllers
{
    [Area("HR"), Authorize]
    [DisplayName("SalaryFormula")]
    public class SalaryFormulaController : SettingsController
    {
        private readonly ISalaryFormula _salaryFormula;
        public SalaryFormulaController(ISalaryFormula salaryFormula)
        {
            _salaryFormula = salaryFormula;
        }
        //[HttpGet]
        //public IActionResult GetSalaryFormula()
        //{
        //    try
        //    {
        //        SalaryFormulaMaster salaryFormulaMaster = new SalaryFormulaMaster();
        //        salaryFormulaMaster = _salaryFormula.GetSalaryFormula().Result;
        //        return View(salaryFormulaMaster);
        //    }
        //    catch (Exception Ex)
        //    {
        //        _logger.LogError(Ex, Ex.Message);
        //        throw;
        //    }
        //}

        //AddUpdateSalaryCalculationFormula
        //[HttpPost]
        //public async Task<ActionResult> AddUpdateSalaryCalculationFormula(SalaryFormulaMaster salaryFormulaMaster)
        //{
        //    try
        //    {
        //        var salaryFormulaId = await _salaryFormula.AddUpdateSalaryCalculationFormula(salaryFormulaMaster);
        //        if (salaryFormulaId > 0)
        //            return Json(new { status = true, message = MessageHelper.SalaryFormulaUpdated });
        //        else
        //            return Json(new { status = false, message = MessageHelper.Error });
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        #region Salary head

        [HttpGet]
        public ActionResult GetAllSalaryHeads([FromQuery(Name = "grid-page")] string gridpage = "1", [FromQuery(Name = "grid-column")] string orderby = "", [FromQuery(Name = "grid-dir")] string sortby = "0", [FromQuery(Name = "grid-filter")] string gridfilter = "", [FromQuery(Name = "grid-search")] string search = "")
        {
            int userid = USERID;
            IQueryCollection query = Request.Query;
            string value = string.Empty;
            Action<IGridColumnCollection<SalaryHead>> columns = c =>
            {
                c.Add(o => o.SrNo, "SrNo").Titled("Sr.No.").SetWidth(20);
                c.Add(o => o.SalaryHeadName).Titled("Salary Head Name").Sortable(true);
                c.Add(o => o.IsActive).Titled("Is Active").Sortable(true);

                //c.Add(o => o.LeaveTypeText).Titled("Leave Type").Sortable(true);
                //c.Add(o => o.NoOfDays).Titled("Days").Sortable(true);
                //c.Add(o => o.LeaveStartDate).Titled("Start Date").Format("{0:dd/MM/yyyy}").Sortable(true);
                //c.Add(o => o.LeaveEndDate).Titled("End Date").Format("{0:dd/MM/yyyy}").Sortable(true);
                //c.Add(o=> o.IsCancel).Titled("Cancel").Encoded(false).Sanitized(false).SetWidth(60).Css("hidden-xs").RenderValueAs(o => $"<input type='checkbox' class='form-check-input' href='javascript:void(0)' data-id='{o.EmployeeLeaveTxnID}' data-key='{o.EmployeeID}' data-value='{o.IsCancel}' onchange='fnIsCancel(this)' id='docCancel' asp-for='{o.IsCancel}' name='{o.IsCancel}'/>");

                c.Add().Titled("Details").Encoded(false).Sanitized(false).SetWidth(60).Css("hidden-xs")
                .RenderValueAs(o => $"<a class='btn' onclick='fnSalaryHeadDetail(this)'" +
                $"href='javascript:void(0)' " +
                $"data-id='{o.SalaryHeadID}' " +
                $"data-bs-toggle='offcanvas' " +
                $"data-bs-target='#canvasSalaryHead' " +
                $"aria-controls='canvasSalaryHead'>" +
                $"<i class='bx bx-edit'></i></a>");
            };
            PagedDataTable<SalaryHead> pds = _salaryFormula.GetAllSalaryHeads(gridpage.ToInt(), PAGESIZE, search, orderby.RemoveSpace(), sortby == "0" ? "ASC" : "DESC").Result;
            var server = new GridCoreServer<SalaryHead>(pds, query, false, "ordersGrid", columns, PAGESIZE, pds.TotalItemCount).Sortable().ClearFiltersButton(true).Selectable(true).WithGridItemsCount().ChangeSkip(false).EmptyText("No record found").ClearFiltersButton(false);
            return View(server.Grid);
        }


        [HttpGet]
        public async Task<PartialViewResult> AddUpdateSalaryHead(int salaryHeadId)
        {
            try
            {
                SalaryHead salaryHead = await _salaryFormula.GetSalaryHeadAsync(salaryHeadId);

                if (salaryHead == null)
                    salaryHead = new SalaryHead { SalaryHeadID = salaryHeadId };

                return PartialView("_addUpdateSalaryHead", salaryHead);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }


        [HttpPost]
        public async Task<ActionResult> AddUpdateSalaryHead(SalaryHead salaryHead)
        {
            try
            {
                salaryHead.CreatedOrModifiedBy = USERID;
                int salaryHeadId = await _salaryFormula.AddUpdateSalaryHead(salaryHead);
                if (salaryHeadId > 0)
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
        #endregion Salary head

        #region Salary Formula

        [HttpGet]
        public ActionResult GetAllSalaryFormula([FromQuery(Name = "grid-page")] string gridpage = "1", [FromQuery(Name = "grid-column")] string orderby = "", [FromQuery(Name = "grid-dir")] string sortby = "0", [FromQuery(Name = "grid-filter")] string gridfilter = "", [FromQuery(Name = "grid-search")] string search = "")
        {
            int userid = USERID;
            IQueryCollection query = Request.Query;
            string value = string.Empty;
            Action<IGridColumnCollection<SalaryFormula>> columns = c =>
            {
                c.Add(o => o.SrNo, "SrNo").Titled("Sr.No.").SetWidth(20);
                c.Add(o => o.EmployeeCategoryText).Titled("Employee Category").Sortable(true);
                c.Add(o => o.EmploymentTypeText).Titled("Employment Type").Sortable(true);
                c.Add(o => o.SalaryHeadName).Titled("Salary Head").Sortable(true);
                c.Add(o => o.SalaryHeadLabel).Titled("Salary Head Label").Sortable(true);
                c.Add(o => o.SalaryTypeText).Titled("Salary Type").Sortable(true);
                c.Add(o => o.SalaryFormulaText).Titled("Salary Formula").Sortable(true);
                c.Add(o => o.LabelPercentage).Titled("Salary Head Percentage").Sortable(true);
                c.Add(o => o.LabelValue).Titled("Salary Head Value").Sortable(true);
                c.Add(o => o.IsActive).Titled("Is Active").Sortable(true);

                //c.Add(o => o.LeaveTypeText).Titled("Leave Type").Sortable(true);
                //c.Add(o => o.NoOfDays).Titled("Days").Sortable(true);
                //c.Add(o => o.LeaveStartDate).Titled("Start Date").Format("{0:dd/MM/yyyy}").Sortable(true);
                //c.Add(o => o.LeaveEndDate).Titled("End Date").Format("{0:dd/MM/yyyy}").Sortable(true);
                //c.Add(o=> o.IsCancel).Titled("Cancel").Encoded(false).Sanitized(false).SetWidth(60).Css("hidden-xs").RenderValueAs(o => $"<input type='checkbox' class='form-check-input' href='javascript:void(0)' data-id='{o.EmployeeLeaveTxnID}' data-key='{o.EmployeeID}' data-value='{o.IsCancel}' onchange='fnIsCancel(this)' id='docCancel' asp-for='{o.IsCancel}' name='{o.IsCancel}'/>");

                c.Add().Titled("Details").Encoded(false).Sanitized(false).SetWidth(60).Css("hidden-xs")
                .RenderValueAs(o => $"<a class='btn' onclick='fnSalaryFormulaDetail(this)'" +
                $"href='javascript:void(0)' " +
                $"data-id='{o.SalaryFormulaID}' " +
                $"data-bs-toggle='offcanvas' " +
                $"data-bs-target='#canvasSalaryFormula' " +
                $"aria-controls='canvasSalaryFormula'>" +
                $"<i class='bx bx-edit'></i></a>");
            };
            PagedDataTable<SalaryFormula> pds = _salaryFormula.GetAllSalaryFormula(0, gridpage.ToInt(), PAGESIZE, search, orderby.RemoveSpace(), sortby == "0" ? "ASC" : "DESC").Result;
            var server = new GridCoreServer<SalaryFormula>(pds, query, false, "ordersGrid", columns, PAGESIZE, pds.TotalItemCount).Sortable().ClearFiltersButton(true).Selectable(true).WithGridItemsCount().ChangeSkip(false).EmptyText("No record found").ClearFiltersButton(false);
            return View(server.Grid);
        }



        [HttpGet]
        public ActionResult Index()
        {
            return View("GetSalaryFormulaByEmployeeCategoryId");
        }

        [HttpGet]
        public ActionResult GetSalaryFormulaByEmployeeCategoryId(int employeeCategoryId, [FromQuery(Name = "grid-page")] string gridpage = "1", [FromQuery(Name = "grid-column")] string orderby = "", [FromQuery(Name = "grid-dir")] string sortby = "0", [FromQuery(Name = "grid-filter")] string gridfilter = "", [FromQuery(Name = "grid-search")] string search = "")
        {
            try
            {
                /* string gridpage = "1";
                 string orderby = "";
                 string sortby = "0";
                 string gridfilter = "";
                 string search = "";
                 int userid = USERID;*/
                IQueryCollection query = Request.Query;
                string value = string.Empty;
                Action<IGridColumnCollection<SalaryFormula>> columns = c =>
                {
                    c.Add(o => o.SrNo, "SrNo").Titled("Sr.No.").SetWidth(20);
                    c.Add(o => o.EmployeeCategoryText).Titled("Employee Category").Sortable(true).SetWidth(150);
                    c.Add(o => o.SalaryHeadName).Titled("Salary Head").Sortable(true).SetWidth(200);
                    c.Add(o => o.SalaryHeadLabel).Titled("Salary Head Label").Sortable(true).SetWidth(130);
                    c.Add(o => o.SalaryFormulaText).Titled("Salary Formula").Sortable(true).SetWidth(170);
                    c.Add(o => o.LabelPercentage).Titled("Salary Head Percentage").Sortable(true).SetWidth(110);
                    c.Add(o => o.LabelValue).Titled("Salary Head Value").Sortable(true).SetWidth(130);
                    c.Add(o => o.IsActive).Titled("Status").Sortable(true);
                    c.Add().Titled("Edit").Encoded(false).Sanitized(false).SetWidth(20).Css("hidden-xs")
                    .RenderValueAs(o => $"<a class='btn' onclick='fnSalaryFormula(this)' href='javascript:void(0)' data-id='{o.SalaryFormulaID}' data-bs-toggle='offcanvas' data-bs-target='#canvasSalaryFormula' aria-controls='canvasSalaryFormula'><i class='bx bx-edit'></i></a>");
                };
                PagedDataTable<SalaryFormula> pds = _salaryFormula.GetAllSalaryFormula(employeeCategoryId, gridpage.ToInt(), PAGESIZE, search, orderby.RemoveSpace(), sortby == "0" ? "ASC" : "DESC").Result;

                var server = new GridCoreServer<SalaryFormula>(pds, query, false, "ordersGrid", columns, PAGESIZE, pds.TotalItemCount)
                    /*.Sortable()
                    .Selectable(true)
                    .ChangeSkip(false)*/
                    .EmptyText("No record found");

                ViewData["EmployeeCategoryID"] = employeeCategoryId;
                return View("GetSalaryFormulaByEmployeeCategoryId", server.Grid);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        [HttpGet]
        public ActionResult GetSalaryFormula(int salaryFormulaId)
        {
            try
            {
                SalaryFormula salaryFormula = _salaryFormula.GetSalaryFormulaAsync(salaryFormulaId).Result;

                if (salaryFormula == null)
                    salaryFormula = new SalaryFormula { SalaryFormulaID = salaryFormulaId };

                return PartialView("_addUpdateSalaryFormula", salaryFormula);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }


        [HttpPost]
        public async Task<ActionResult> AddUpdateSalaryFormula(SalaryFormula salaryFormula)
        {
            try
            {
                salaryFormula.CreatedOrModifiedBy = USERID;
                int salaryFormulaId = await _salaryFormula.AddUpdateSalaryFormula(salaryFormula);
                if (salaryFormulaId > 0)
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
        #endregion Salary Formula
    }
}
