using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using ERP.Controllers;
using Business.SQL;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Business.Entities.LeaveTypeMaster;
using GridShared;
using GridCore.Server;
using Business.Interface.ILeaveMaster;
using Microsoft.AspNetCore.Authorization;
using ERP.Helpers;
using Business.Entities.EmployeeLeaveTxn;
using Microsoft.AspNetCore.Mvc.Rendering;
using Business.Interface;
using Business.Interface.IEmployee;
using Business.Entities.Employee;
using System.Linq;
using Business.Service;

namespace ERP.Areas.HR.Controllers
{
    [Area("HR"), Authorize]
    public class LeaveMasterController : SettingsController
    {
        private readonly IConfiguration _configuration;
        private readonly ILeaveMasterService _leaveMasterService;
        private readonly IMasterService _masterService;
        private readonly IEmployeeService _employeeService;
        public LeaveMasterController(IConfiguration configuration, ILeaveMasterService leaveMasterService, IMasterService masterService, IEmployeeService employeeService)
        {
            _configuration = configuration;
            _leaveMasterService = leaveMasterService;
            _masterService = masterService;
            _employeeService = employeeService;
        }

        [HttpGet]
        public ActionResult Index([FromQuery(Name = "grid-page")] string gridpage = "1", [FromQuery(Name = "grid-column")] string orderby = "", [FromQuery(Name = "grid-dir")] string sortby = "0", [FromQuery(Name = "grid-filter")] string gridfilter = "", [FromQuery(Name = "grid-search")] string search = "")
        {
            int userid = USERID;
            IQueryCollection query = Request.Query;
            string value = string.Empty;
            Action<IGridColumnCollection<LeaveTypeMaster>> columns = c =>
            {
                c.Add(o => o.SrNo, "SrNo").Titled("Sr.No.").SetWidth(20);
                c.Add(o => o.LeaveTypeText).Titled("Leave Type").Sortable(true);
                c.Add(o => o.IsActive).Titled("IsActive").Sortable(true);
                //Below code hide on phones
                c.Add().Titled("Details").Encoded(false).Sanitized(false).SetWidth(60).Css("hidden-xs")
                .RenderValueAs(o => $"<a class='btn' onclick='fnLeaveTypeMaster(this)' href='javascript:void(0)' data-id='{o.LeaveTypeID}' data-bs-toggle='offcanvas' data-bs-target='#canvasLeaveTypeMaster' aria-controls='canvasLeaveTypeMaster'><i class='bx bx-edit'></i> </a>");
            };
            PagedDataTable<LeaveTypeMaster> pds = _leaveMasterService.GetAllLeaveTypeMaster(gridpage.ToInt(), PAGESIZE, search, orderby.RemoveSpace(), sortby == "0" ? "ASC" : "DESC").Result;
            var server = new GridCoreServer<LeaveTypeMaster>(pds, query, false, "ordersGrid",
                columns, PAGESIZE, pds.TotalItemCount)
                .Sortable().ClearFiltersButton(true).Selectable(true).WithGridItemsCount().ChangeSkip(false).EmptyText("No record found").ClearFiltersButton(false);
            return View(server.Grid);
        }
        #region Leave type master

        [HttpGet]
        public async Task<PartialViewResult> AddUpdateLeaveType(int leaveTypeId)
        {
            try
            {
                LeaveTypeMaster leaveTypeMaster = await _leaveMasterService.GetLeaveTypeAsync(leaveTypeId);

                if (leaveTypeMaster == null)
                    leaveTypeMaster = new LeaveTypeMaster { LeaveTypeID = leaveTypeId };

                return PartialView("_addUpdateLeaveType", leaveTypeMaster);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddUpdateLeaveType(LeaveTypeMaster leaveTypeMaster)
        {
            try
            {
                leaveTypeMaster.CreatedOrModifiedBy = USERID;
                int leaveTypeId = await _leaveMasterService.AddUpdateLeaveType(leaveTypeMaster);
                if (leaveTypeId > 0)
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
        #endregion Leave type master

        #region Employee leave transaction
        [HttpGet]
        public ActionResult GetAllEmployeeLeaveTxn([FromQuery(Name = "grid-page")] string gridpage = "1", [FromQuery(Name = "grid-column")] string orderby = "", [FromQuery(Name = "grid-dir")] string sortby = "0", [FromQuery(Name = "grid-filter")] string gridfilter = "", [FromQuery(Name = "grid-search")] string search = "")
        {
            int userid = USERID;
            IQueryCollection query = Request.Query;
            string value = string.Empty;
            Action<IGridColumnCollection<EmployeeLeaveTxn>> columns = c =>
            {
                c.Add(o => o.SrNo, "SrNo").Titled("Sr.No.").SetWidth(20);
                c.Add(o => o.FinancialYear).Titled("Financial Year").Sortable(true);
                c.Add(o => o.EmployeeName).Titled("Employee Name").Sortable(true);
                c.Add(o => o.LeaveTypeText).Titled("Leave Type").Sortable(true);
                c.Add(o => o.NoOfDays).Titled("Days").Sortable(true);
                c.Add(o => o.LeaveStartDate).Titled("Start Date").Format("{0:dd/MM/yyyy}").Sortable(true);
                c.Add(o => o.LeaveEndDate).Titled("End Date").Format("{0:dd/MM/yyyy}").Sortable(true);

                //c.Add(o=> o.IsCancel).Titled("Cancel").Encoded(false).Sanitized(false).SetWidth(60).Css("hidden-xs").RenderValueAs(o => $"<input type='checkbox' class='form-check-input' href='javascript:void(0)' data-id='{o.EmployeeLeaveTxnID}' data-key='{o.EmployeeID}' data-value='{o.IsCancel}' onchange='fnIsCancel(this)' id='docCancel' asp-for='{o.IsCancel}' name='{o.IsCancel}'/>");

                c.Add().Titled("Details").Encoded(false).Sanitized(false).SetWidth(60).Css("hidden-xs")
                .RenderValueAs(o => $"<a class='btn' onclick='fnEmployeeLeaveTxn(this)'" +
                $"href='javascript:void(0)' " +
                $"data-id='{o.EmployeeLeaveTxnID}' " +
                $"data-key='{o.EmployeeID}' " +
                $"data-bs-toggle='offcanvas' " +
                $"data-bs-target='#canvasEmployeeLeaveTxn' " +
                $"aria-controls='canvasEmployeeLeaveTxn'>" +
                $"<i class='bx bx-edit'></i></a>");
            };
            PagedDataTable<EmployeeLeaveTxn> pds = _leaveMasterService.GetAllEmployeeLeaveTxn(gridpage.ToInt(), PAGESIZE, search, orderby.RemoveSpace(), sortby == "0" ? "ASC" : "DESC").Result;
            var server = new GridCoreServer<EmployeeLeaveTxn>(pds, query, false, "ordersGrid",
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
        public async Task<PartialViewResult> AddUpdateEmployeeLeaveTxn(int employeeLeaveTxnID, int empliyeeId)
        {
            try
            {
                EmployeeLeaveTxn employeeLeaveTxn = await _leaveMasterService.GetEmployeeLeaveTxnAsync(employeeLeaveTxnID, empliyeeId);

                if (employeeLeaveTxn == null)
                    employeeLeaveTxn = new EmployeeLeaveTxn { EmployeeLeaveTxnID = employeeLeaveTxnID, EmployeeID = empliyeeId };


                var FinancialYearList = _masterService.GetAllFinancialYearMasterAsync();
                ViewData["FinancialYear"] = new SelectList(FinancialYearList, "FinancialYearID", "FinancialYear");

                var leaveTypeList = _leaveMasterService.GetAllLeaveTypeMaster().Result;
                ViewData["LeaveType"] = new SelectList(leaveTypeList, "LeaveTypeID", "LeaveTypeText");

                var employeeList = _employeeService.GetAllEmployeesAsync().Result;
                ViewData["EmployeeList"] = new SelectList(employeeList, "EmployeeID", "EmployeeName");

                return PartialView("_addUpdateEmployeeLeaveTxn", employeeLeaveTxn);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddUpdateEmployeeLeaveTxn(EmployeeLeaveTxn employeeLeaveTxn)
        {
            try
            {
                employeeLeaveTxn.CreatedOrModifiedBy = USERID;
                int employeeLeaveTxnID = await _leaveMasterService.AddUpdateEmployeeLeaveTxn(employeeLeaveTxn);
                if (employeeLeaveTxnID > 0)
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

        [HttpPost]
        public async Task<ActionResult> CancelEmployeeLeave(int employeeLeaveTxnID, int empliyeeId, bool isCancel)
        {
            EmployeeLeaveTxn employeeLeaveTxn = new EmployeeLeaveTxn()
            {
                EmployeeLeaveTxnID = employeeLeaveTxnID,
                EmployeeID = empliyeeId,
                IsCancel = isCancel,
                CreatedOrModifiedBy = USERID
            };

            int employeeLeaveCancel = await _leaveMasterService.CancelEmployeeLeave(employeeLeaveTxn);

            if (employeeLeaveCancel > 0)
            {
                if (isCancel)
                    return Json(new { status = true, message = MessageHelper.LeaveCanceled });
                else
                    return Json(new { status = true, message = MessageHelper.LeaveApply });
            }
            else
                return Json(new { status = true, message = MessageHelper.Error });
        }

        [HttpPost]
        public JsonResult SearchEmployee(string prefix)
        {
            try
            {
                var empName = _masterService.GetEmployeesByName(prefix);

                return Json(empName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        [HttpPost]
        public JsonResult AutoComplete(string prefix)
        {
            try
            {
                //var zipcodeData = new Business.Service.MasterService(_config).GetZipCodeAsync(prefix);
                var zipcodes = _masterService.GetEmployeesByName(prefix).Select(y=>y.EmployeeName);
                //var zipcodes = (from zip in Employees
                //                where zip.ZipCode.StartsWith(prefix)
                //                select new
                //                {
                //                    label = zip.ZipCode,
                //                    val = zip.ZipCodeID
                //                }).ToList();

                return Json(zipcodes);
            }
            catch
            {
                return Json(false);
            }
        }


        #endregion Employee leave transaction
    }
}
