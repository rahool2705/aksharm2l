using Business.SQL;
using ClosedXML.Excel;
using ERP.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Threading.Tasks;

namespace ERP.Areas.HR.Controllers
{
    [Area("HR"), Authorize]
    public class ImportEmployeeRecordsController : SettingsController
    {
        private IConfiguration _configuration { get; set; }
        private string connection = string.Empty;
        private readonly IHostEnvironment _hostEnvironment;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string linkDocument;
        public ImportEmployeeRecordsController(IHostEnvironment hostEnvironment, IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _configuration = configuration;
            connection = _configuration.GetConnectionString("DefaultConnection");
            _hostEnvironment = hostEnvironment;
            _webHostEnvironment = webHostEnvironment;
            linkDocument = _configuration.GetSection("ImportMultipleEmployeeData")["ImportDocuments"];
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> GetExcelData(IFormFile file)
        {
            int userId = 1;
            DataTable dt = new DataTable();
            string filepath = string.Empty;
            if (file != null)
            {
                var path1 = "";
                //string fileExtension = Path.GetExtension(file.FileName);

                path1 = _webHostEnvironment.WebRootPath + linkDocument;  //full path Excluding file name ----  0
                //string filepath = path1;  //full path including file name  -----  1
                //string filename = file + fileExtension;
                //string dbfilepath = linkEmployeeDocument + filename;
                filepath = path1 + file.FileName;
                if (!Directory.Exists(path1))
                {
                    Directory.CreateDirectory(path1);
                }
                if (Directory.Exists(path1))
                {
                    using (var fileStream = new FileStream(filepath, FileMode.Create, FileAccess.ReadWrite))
                    {
                        file.CopyTo(fileStream);
                    }
                }
            }

            //string path = "C:\\Users\\Lenovo\\Downloads\\Employee Temaplate for Importing Records_org.xlsx";
            List<string> strforRow = new List<string>();
            //Checking file content length and Extension must be .xlsx  
            //if (file != null && file.Length> 0 && Path.GetExtension(file.FileName).ToLower() == ".xlsx")
            if (!string.IsNullOrEmpty(filepath))
            {
                //string path = Path.Combine(Microsoft.AspNetCore.Components.Server.MapPath("~/UploadFile"), Path.GetFileName(file.FileName));

                //Saving the file  
                //var isSave = file.SaveAs(path);
                //Started reading the Excel file.  
                using (XLWorkbook workbook = new XLWorkbook(filepath))
                {
                    IXLWorksheet worksheet = workbook.Worksheet(1);
                    bool FirstRow = true;

                    //Range for reading the cells based on the last cell used.  
                    string readRange = "1:1";

                    foreach (IXLRow row in worksheet.RowsUsed())
                    {
                        //If Reading the First Row (used) then add them as column name  
                        if (FirstRow)
                        {
                            //Checking the Last cellused for column generation in datatable  
                            readRange = string.Format("{0}:{1}", 1, row.LastCellUsed().Address.ColumnNumber);
                            foreach (IXLCell cell in row.Cells(readRange))
                            {
                                dt.Columns.Add(cell.Value.ToString());
                            }
                            FirstRow = false;
                        }
                        else
                        {
                            //Adding a Row in datatable  
                            dt.Rows.Add();
                            int cellIndex = 0;
                            //Updating the values of datatable  
                            foreach (IXLCell cell in row.Cells(readRange))
                            {
                                //foreach (var items in param)
                                //{
                                //    items.Value = cell.Value.ToString();
                                //}
                                //return obj != null ? Convert.ToInt32(obj) : 0;

                                dt.Rows[dt.Rows.Count - 1][cellIndex] = cell.Value.ToString();

                                cellIndex++;
                            }

                        }
                    }
                    //If no data in Excel file  
                    if (FirstRow)
                    {
                        ViewBag.Message = "Empty Excel File!";
                    }
                }
            }
            return View("Index",dt);
        }

        [HttpPost]
        public async Task<IActionResult> ImportEmployeeData(string dataTable)
        {
            DataTable data = JsonConvert.DeserializeObject<DataTable>(dataTable);
            var emp = data;
            if (data.Rows.Count > 0)
            {
                //      KEEP ALL COMMENTED CODE IN THIS METHOD IT IS FOR FUTURE DEVELOPMENT OF PROGRESS BAR FOR PROCESSING TIME, COMMENTED BY RAHUL MISTRY ON 31-MARCH-2023.

                //var hubContext = HttpContext.RequestServices.GetRequiredService<IHubContext<ProgressHub>>();
                //var progress = hubContext.Clients.All.SendAsync("progressUpdate", 0);
                decimal totalItems = 0;
                decimal i = 1;
                SqlParameter[] param1 = new SqlParameter[1];
                totalItems = data.Rows.Count;
                decimal percentComplete = 1;
                foreach (DataRow dr in data.Rows)
                {
                    SqlParameter[] param = {
                                         new SqlParameter("@PrefixName", dr["Prefix"].ToString())
                                        ,new SqlParameter("@FirstName", dr["FirstName"].ToString())
                                        ,new SqlParameter("@MiddleName", dr["MiddleName"].ToString())
                                        ,new SqlParameter("@LastName", dr["LastName"].ToString())
                                        ,new SqlParameter("@EmployeeCode", dr["EmployeeCode"].ToString())
                                        ,new SqlParameter("@Gender", dr["Gender"].ToString())
                                        ,new SqlParameter("@EmployeeBloodGroup", dr["EmployeeBloodGroup"].ToString())
                                        ,new SqlParameter("@JobTitle", dr["JobTitle"].ToString())
                                        ,new SqlParameter("@Department",  dr["Department"].ToString())
                                        ,new SqlParameter("@Designation", dr["Designation"].ToString())
                                        ,new SqlParameter("@ReportingTo", dr["ReportingTo"].ToString())
                                        ,new SqlParameter("@PersonalMobileNo", dr["PersonalMobileNo"].ToString())
                                        ,new SqlParameter("@OfficeMobileNo", dr["OfficeMobileNo"].ToString())
                                        ,new SqlParameter("@AlternativeMobileNo", dr["AlternativeMobileNo"].ToString())
                                        ,new SqlParameter("@Note", dr["Note"].ToString())
                                        ,new SqlParameter("@PersonalEmail", dr["PersonalEmail"].ToString())
                                        ,new SqlParameter("@OfficeEmail", dr["OfficeEmail"].ToString())
                                        ,new SqlParameter("@BirthDate", dr["DOB"].ToString())
                                        ,new SqlParameter("@Religion", dr["Religion"].ToString())
                                        ,new SqlParameter("@ReferenceBy", dr["ReferenceBy"].ToString())
                                        ,new SqlParameter("@ReferenceContact", dr["ReferenceContact"].ToString())
                                        ,new SqlParameter("@CompanyID", COMPANYID)
                                        ,new SqlParameter("@CreatedOrModifiedby", USERID)
                                    };
                    var obj = await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_I_EmployeeDetailUpload", param);

                    //percentComplete = ((i * 100) / totalItems);
                    //await hubContext.Clients.All.SendAsync("progressUpdate", percentComplete);

                    i = i + 1;
                }
                //var test = await progress.ContinueWith(task => task.Result.SendAsync("progressUpdate", percentComplete));
            }

            else
            {
                //If file extension of the uploaded file is different then .xlsx  
                ViewBag.Message = "Please select file with .xlsx extension!";
            }
            ////return View("Index", dt);
            return Json(new {status=true,message = "Data imported to database."});
        }
    }
}
