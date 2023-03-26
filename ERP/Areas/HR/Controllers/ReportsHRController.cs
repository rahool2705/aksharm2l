using Business.Entities.EmployeeAttendanceSummary;
using Business.Interface;
using Business.Interface.IEmployeeAttendanceSummary;
using ClosedXML.Excel;
using ERP.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;
using System.IO;
using System.Linq;

namespace ERP.Areas.HR.Controllers
{
    [Area("HR"), Authorize]
    public class ReportsHRController : SettingsController
    {
        private readonly IEmployeeAttendanceSummaryService _employeeAttendanceSummaryService;
        private readonly IMasterService _masterService;
        public ReportsHRController(IEmployeeAttendanceSummaryService employeeAttendanceSummaryService, IMasterService masterService)
        {
            _employeeAttendanceSummaryService = employeeAttendanceSummaryService;
            _masterService = masterService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetEmployeeAttendanceSummary(int employeeCategoryId, int employeeId, int month, int year, int departmentId, string searchString, bool isDownload)

        {
            try
            {
                month = month <= 0 ? DateTime.Now.Month : month;
                year = year <= 0 ? DateTime.Now.Year : year;

                ViewData["EmployeeCategoryID"] = employeeCategoryId;
                ViewData["MonthYear"] = new DateTime(year, month, 1);
                ViewData["DepartmentID"] = departmentId;
                ViewData["SearchString"] = searchString;

                DataSet dataSet = _employeeAttendanceSummaryService.GetEmployeeAllAttendanceSummary(employeeCategoryId, employeeId, month, year, departmentId, searchString).Result;

                if (isDownload && dataSet != null)
                    return ExportToExcel(dataSet);

                if (dataSet.Tables.Count > 0)
                    return View(dataSet);

                else return View("GetEmployeeAttendanceSummary");
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public IActionResult GetEmployeeDetailSummary(int employeeCategoryId, int departmentId, string searchstring)

        {
            try
            {

                DataSet dataSet = _employeeAttendanceSummaryService.GetEmployeeAllDetailSummary(employeeCategoryId, departmentId, searchstring).Result;

                if (dataSet.Tables.Count > 0)
                    return View(dataSet);

                else return View("GetEmployeeDetailSummary");
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpGet]
        public JsonResult GetEmployees(string empString)
        {
            try
            {
                var employees = _masterService.GetEmployeesByName(empString);
                var employeeresult = employees.Select(x => new { label = x.EmployeeName, val = x.EmployeeID });
                return Json(employeeresult);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //[HttpGet]
        //public JsonResult GetEmployeeslist(string empString)
        //{
        //    try
        //    {
        //        var employeesData = _masterService.GetEmployeesByName(empString);
        //        var EmpIds = (from emp in employeesData
        //                        where emp.EmployeeName.StartsWith(empString)
        //                        select new
        //                        {
        //                            label = emp.EmployeeName,
        //                            val = emp.EmployeeID
        //                        }).ToList();
        //        return Json(EmpIds);
        //    }
        //    catch
        //    {
        //        return Json(false);
        //    }
        //}

        #region Export to excel 
        public IActionResult ExportToExcel(DataSet dataSet)
        {
            using (XLWorkbook wb = new XLWorkbook())
            {
                int month = DateTime.Now.Month;
                int year = DateTime.Now.Year;
                //DataSet dt = _employeeAttendanceSummaryService.GetEmployeeAllAttendanceSummary(0, 0, month, year).Result;
                wb.Worksheets.Add(dataSet);
                using (MemoryStream stream = new MemoryStream())
                {

                    wb.SaveAs(stream);

                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Employee-Attendance" + DateTime.Now + ".xlsx");
                }
            }

        }
        #endregion Export to excel

    }
}
