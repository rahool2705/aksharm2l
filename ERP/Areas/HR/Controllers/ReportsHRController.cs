using AspNetCoreHero.ToastNotification.Helpers;
using Business.Entities.EmployeeAttendanceSummary;
using Business.Entities.Master.EmployeeCategory;
<<<<<<< Updated upstream
=======
using Business.Entities.Master.EmployementType;
>>>>>>> Stashed changes
using Business.Interface;
using Business.Interface.IEmployeeAttendanceSummary;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Bibliography;
using ERP.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Constraints;
using System;
using System.Data;
using System.Globalization;
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
                int userId = USERID;
                month = month <= 0 ? DateTime.Now.Month : month;
                year = year <= 0 ? DateTime.Now.Year : year;

                ViewData["EmployeeCategoryID"] = employeeCategoryId;
                ViewData["MonthYear"] = new DateTime(year, month, 1);
                ViewData["DepartmentID"] = departmentId;
                ViewData["SearchString"] = searchString;

                DataSet dataSet = _employeeAttendanceSummaryService.GetEmployeeAllAttendanceSummary(employeeCategoryId, userId, month, year, departmentId, searchString).Result;

                if (dataSet.Tables.Count > 0)
                {
                    if (isDownload)
                    {
                        return ExportToExcel(dataSet, "EmployeesAttendanceList");
                    }
                    else
                    {
                        return View(dataSet);
                    }
                }
                else
                {
                    return View("GetEmployeeAttendanceSummary");
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public IActionResult GetEmployeeDetailSummary(int employeeCategoryId, int departmentId, string searchstring, bool isDownload)

        {
            try
            {
                ViewData["EmployeeCategoryID"] = employeeCategoryId;
                ViewData["DepartmentID"] = departmentId;
                ViewData["SearchString"] = searchstring;
                DataSet dataSet = _employeeAttendanceSummaryService.GetEmployeeAllDetailSummary(employeeCategoryId, departmentId, searchstring).Result;

                if (dataSet.Tables.Count > 0)
                {
                    if (isDownload)
                    {
                        return ExportToExcel(dataSet, "EmployeesList");
                    }
                    else
                    {
                        return View(dataSet);
                    }
                }
                else
                {
                    return View("GetEmployeeDetailSummary");
                }
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
        public IActionResult ExportToExcel(DataSet dataSet, string filename)
        {
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dataSet);
                using (MemoryStream stream = new MemoryStream())
                {

                    wb.SaveAs(stream);

                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", filename + DateTime.Now + ".xlsx");
                }
            }

        }
        #endregion Export to excel


        #region Employee Salary Summuary

        [HttpGet]
        public IActionResult GetEmployeeSalarySummary(int employeeCategoryId, int companyId, int month, int year, int employeeId, bool isDownload, int employmentTypeId, DateTime salaryDate, int isSalProcess)
        {
            //int employmentTypeId,DateTime salaryDate
            try
            {
                //string dateString = "Tue Apr 04 2023 16:44:09 GMT+0530 (India Standard Time)";
                //DateTime dateTime = DateTime.Parse(salaryDate, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal);

                int userId = USERID;
                month = month <= 0 ? DateTime.Now.Month : month;
                year = year <= 0 ? DateTime.Now.Year : year;

                ViewData["EmployeeCategoryID"] = employeeCategoryId;
                ViewData["MonthYear"] = new DateTime(year, month, 1);
                ViewData["EmployeeID"] = employeeId;
                ViewData["CompanyID"] = companyId == 0 ? COMPANYID : companyId;
                ViewData["EmploymentTypeID"] = employmentTypeId;

                if (isSalProcess == 1)
                {
                    DataSet dataSet = _employeeAttendanceSummaryService.ProcesSalary(year, month, companyId, employmentTypeId, employeeCategoryId, userId, salaryDate).Result;
                    var test = dataSet;
                    return View(dataSet);
                }
                else
                {
                    DataSet dataSet = _employeeAttendanceSummaryService.GetEmployeeSalarySummary(employeeCategoryId, userId, companyId, month, year, employeeId).Result;
                    if (dataSet.Tables.Count > 0)
                    {
                        if (isDownload)
                        {
                            return ExportToExcel(dataSet, "GetEmployeeSalarySummary");
                        }
                        else
                        {
                            return View(dataSet);
                        }
                    }
                    else
                    {
                        return View("GetEmployeeSalarySummary");
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        #endregion Employee Salary Summuary

        #region Salary Process
        [HttpPost]
        public IActionResult RunSalaryProcess()
        {
            return View("GetEmployeeSalarySummary");
        }

        #endregion Salary Process

        #region Salary Process Edit
        [HttpGet]
        public IActionResult Edit(int editId)
        {
           // _employeeAttendanceSummaryService.GetEmployeeSalarySummaryEdit(editId);
            return View("EditEmployeeSalaryRecord");
        }

        #endregion Salary Process Edit

<<<<<<< Updated upstream
=======
        #region Varify Salary

        [HttpPost]
        public async Task<JsonResult> VerifySalary(int year, int month, int companyId, int employeeId, int employeeCategoryId)
        {
            try
            {
                int userId = USERID;
                var result = await _employeeAttendanceSummaryService.VerifyEmplyeeSalary(year, month, companyId, employeeId, employeeCategoryId, userId);

                if (result > 0)
                    return Json(new { status = true, MessageHelper.Updated });

                else
                    return Json(new { status = true, MessageHelper.Error });
            }
            catch (Exception)
            {

                throw;
            }
        }


        [HttpPost]
        public async Task<IActionResult> UpdateVerifiedSalaryTable(List<UpdateSalary> dataTable)
        {
            try
            {
                var list = dataTable;
                //var test1 = JsonConvert.DeserializeObject<DataTable>(dataTable);
                //DataTable data = JsonConvert.DeserializeObject<DataTable>(dataTable);
                //var emp = data;
                return View();
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion Varify Salary


    }
    public class UpdateSalary
    {
        public string SalaryHeadLabel { get; set; }
        public string SalaryHeadName { get; set; }
        public string CalculatedValue { get; set; }
        public string SalaryFormula { get; set; }
>>>>>>> Stashed changes
    }
}
