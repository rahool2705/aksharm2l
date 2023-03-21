using Business.Entities.EmployeeAttendanceSummary;
using Business.Interface;
using Business.Interface.IEmployeeAttendanceSummary;
using ERP.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;
using System.Linq;

namespace ERP.Areas.HR.Controllers
{
    [Area("HR"), Authorize]
    public class ReportsController : SettingsController
    {
        private readonly IEmployeeAttendanceSummaryService _employeeAttendanceSummaryService;
        private readonly IMasterService _masterService;
        public ReportsController(IEmployeeAttendanceSummaryService employeeAttendanceSummaryService, IMasterService masterService)
        {
            _employeeAttendanceSummaryService = employeeAttendanceSummaryService;
            _masterService = masterService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetEmployeeAttendanceSummary(int employeeCategoryId, int employeeId, int month, int year)
        
        {
            try
            {
                month = month <= 0 ? DateTime.Now.Month : month;
                year = year <= 0 ? DateTime.Now.Year : year;
                DataSet test = _employeeAttendanceSummaryService.GetEmployeeAllAttendanceSummary(employeeCategoryId, employeeId, month, year).Result;

                /*   var result = test.Result;
                   var newColumns = result.Tables[0].Columns;
                   var newRows = result.Tables[0].Rows;
                   DataTable dataTable = new DataTable();
                   foreach (var columns in newColumns)
                   {
                       dataTable.Columns.Add(columns.ToString());
                       foreach (var rows in newRows)
                       {
                           dataTable.Rows.Add(rows.ToString());
                       }
                   }
                   var testTable = dataTable;
                   var resultTable = testTable;    */

                return View(test);
            }
            catch (System.Exception ex)
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


    }
}
