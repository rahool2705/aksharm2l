using Business.Interface.CanteenCharges;
using Business.Interface;
using ERP.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using Business.Interface.ILoanAdvanceService;
using Business.Entities.CanteenCharges;
using Business.Service;
using Business.SQL;
using ERP.Helpers;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System;
using Business.Entities.EmployeeLoan;
using GridCore.Server;
using GridShared;
using Microsoft.AspNetCore.Mvc.Rendering;
using Business.Entities.EmployeeAdvances;

namespace ERP.Areas.HR.Controllers
{
    [Area("HR"), Authorize]
    [DisplayName("LoanAdvance")]
    public class LoanAdvanceController : SettingsController
    {
        private readonly ILoanAdvanceService _loanAdvanceService;
        private readonly IMasterService _masterService;


        public LoanAdvanceController(ILoanAdvanceService loanAdvanceService, IMasterService masterService)
        {
            _loanAdvanceService = loanAdvanceService;
            _masterService = masterService;
        }
        [HttpGet]
        public ActionResult Index([FromQuery(Name = "grid-page")] string gridpage = "1", [FromQuery(Name = "grid-column")] string orderby = "", [FromQuery(Name = "grid-dir")] string sortby = "0", [FromQuery(Name = "grid-filter")] string gridfilter = "", [FromQuery(Name = "grid-search")] string search = "")
        {
            int userid = USERID;
            IQueryCollection query = Request.Query;
            string value = string.Empty;

            Action<IGridColumnCollection<EmployeeLoan>> columns = c =>
            {
                c.Add(o => o.SrNo).Titled("Sr No.").Sortable(true);
                c.Add(o => o.EmployeeName).Titled("Employee Name").Sortable(true);
                c.Add(o => o.PaymentDate).Titled("Payment Date").Sortable(true).Format("{0:dd/MM/yyyy}");
                c.Add(o => o.EmployeeLoanAmount).Titled("Employee Loan Amount").Sortable(true);
                c.Add(o => o.InterestRate).Titled("Interest Rate").Sortable(true);
                c.Add(o => o.TenureMonths).Titled("Tenure Months").Sortable(true);
                c.Add(o => o.AdjustmentAmount).Titled("Adjustment Amount").Sortable(true);
                //c.Add(o => o.IsActive).Titled("Status").Sortable(true);
                //Below code hide on phones
                c.Add().Titled("Edit").Encoded(false).Sanitized(false).SetWidth(60).Css("hidden-xs")
                .RenderValueAs(o => $"<a class='btn' onclick='fnEmployeeLoan(this)' href='javascript:void(0)' data-id='{o.EmployeeLoanID}' data-bs-toggle='offcanvas' data-bs-target='#canvasEmployeeLoan' aria-controls='canvasEmployeeLoan'><i class='bx bx-edit'></i> </a>");
            };
            PagedDataTable<EmployeeLoan> pds = _loanAdvanceService.GetAllEmployeeLoanAsync(gridpage.ToInt(), PAGESIZE, search, orderby.RemoveSpace(), sortby == "0" ? "ASC" : "DESC").Result;
            var server = new GridCoreServer<EmployeeLoan>(pds, query, false, "ordersGrid",
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

        #region Employee Loan

        [HttpGet]
        public async Task<PartialViewResult> AddUpdateEmployeeLoan(int employeeLoanID)
        {
            try
            {
                EmployeeLoan employeeLoan = await _loanAdvanceService.GetEmployeeLoanAsync(employeeLoanID);

                if (employeeLoan == null)
                    employeeLoan = new EmployeeLoan { EmployeeLoanID = employeeLoanID };

                var listEmployees = _masterService.GetAllEmployees();
                ViewData["EmployeeName"] = new SelectList(listEmployees, "EmployeeID", "EmployeeName");

                return PartialView("_addUpdateEmployeeLoan", employeeLoan);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddUpdateEmployeeLoan(EmployeeLoan employeeLoan)
        {
            try
            {
                employeeLoan.CreatedOrModifiedBy = USERID;
                int employeeLoanID = await _loanAdvanceService.EmployeeLoanCreateOrUpdateAsync(employeeLoan);
                if (employeeLoanID > 0)
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

        #endregion Employee Loan

        #region Employee Advance

        public ActionResult GetAllEmployeeAdvances([FromQuery(Name = "grid-page")] string gridpage = "1", [FromQuery(Name = "grid-column")] string orderby = "", [FromQuery(Name = "grid-dir")] string sortby = "0", [FromQuery(Name = "grid-filter")] string gridfilter = "", [FromQuery(Name = "grid-search")] string search = "")
        {
            int userid = USERID;
            IQueryCollection query = Request.Query;
            string value = string.Empty;

            Action<IGridColumnCollection<EmployeeAdvances>> columns = c =>
            {
                c.Add(o => o.SrNo).Titled("Sr No.").Sortable(true);
                c.Add(o => o.EmployeeName).Titled("Employee Name").Sortable(true);
                c.Add(o => o.PaymentDate).Titled("Payment Date").Sortable(true).Format("{0:dd/MM/yyyy}");
                c.Add(o => o.Amount).Titled("Employee Amount Amount").Sortable(true);
                c.Add(o => o.Description).Titled("Description").Sortable(true);
                //c.Add(o => o.IsActive).Titled("Status").Sortable(true);
                //Below code hide on phones
                c.Add().Titled("Edit").Encoded(false).Sanitized(false).SetWidth(60).Css("hidden-xs")
                .RenderValueAs(o => $"<a class='btn' onclick='fnEmployeeAdvances(this)' href='javascript:void(0)' data-id='{o.EmployeeAdvancesID}' data-bs-toggle='offcanvas' data-bs-target='#canvasEmployeeAdvances' aria-controls='canvasEmployeeAdvances'><i class='bx bx-edit'></i> </a>");
            };
            PagedDataTable<EmployeeAdvances> pds = _loanAdvanceService.GetAllEmployeeAdvancesAsync(gridpage.ToInt(), PAGESIZE, search, orderby.RemoveSpace(), sortby == "0" ? "ASC" : "DESC").Result;
            var server = new GridCoreServer<EmployeeAdvances>(pds, query, false, "ordersGrid",
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

        [HttpGet]
        public async Task<PartialViewResult> AddUpdateEmployeeAdvances(int employeeAdvancesID)
        {
            try
            {
                EmployeeAdvances employeeAdvances = await _loanAdvanceService.GetEmployeeAdvancesAsync(employeeAdvancesID);

                if (employeeAdvances == null)
                    employeeAdvances = new EmployeeAdvances { EmployeeAdvancesID = employeeAdvancesID };

                var listEmployees = _masterService.GetAllEmployees();
                ViewData["EmployeeName"] = new SelectList(listEmployees, "EmployeeID", "EmployeeName");

                return PartialView("_addUpdateEmployeeAdvances", employeeAdvances);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddUpdateEmployeeAdvances(EmployeeAdvances employeeAdvances)
        {
            try
            {
                employeeAdvances.CreatedOrModifiedBy = USERID;
                int employeeAdvancesID = await _loanAdvanceService.EmployeeAdvancesCreateOrUpdateAsync(employeeAdvances);
                if (employeeAdvancesID > 0)
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
        #endregion Employee Advance
    }
}
