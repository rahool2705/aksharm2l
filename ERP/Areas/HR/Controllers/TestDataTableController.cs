using Business.Interface.HR;
using Business.Interface.IEmployee;
using Business.Interface;
using ERP.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.ComponentModel;
using Microsoft.AspNetCore.Authorization;

namespace ERP.Areas.HR.Controllers
{
    [Area("HR"), Authorize]
    [DisplayName("TestDataTable")]
    public class TestDataTableController : SettingsController
    {
        private readonly IConfiguration _configuration;
        private readonly IHRConfigService _hRConfigService;
        private readonly IMasterService _masterService;
        private readonly IEmployeeService _employeeService;

        public TestDataTableController(IConfiguration configuration, IHRConfigService hRConfigService, IMasterService masterService, IEmployeeService employeeService)
        {
            _configuration = configuration;
            _hRConfigService = hRConfigService;
            _masterService = masterService;
            _employeeService = employeeService;

        }

        #region Below code is for testing porpose for Jquery DataTables
        //[HttpGet]
        //public IActionResult Index()
        //{
        //    return View();
        //}
        ///// <summary>Loads the table.</summary>
        ///// <param name="dtParameters">The dt parameters.</param>
        ///// <returns>
        /////   <br />
        ///// </returns>
        //[HttpPost("LoadTable")]
        //public async Task<IActionResult> LoadTable([FromBody] DtParameters dtParameters)
        //{
        //    var searchBy = dtParameters.Search?.Value;

        //    // if we have an empty search then just order the results by Id ascending
        //    var orderCriteria = "SrNo";
        //    var orderAscendingDirection = true;

        //    if (dtParameters.Order != null)
        //    {
        //        // in   example we just default sort on the 1st column
        //        orderCriteria = dtParameters.Columns[dtParameters.Order[0].Column].Data;
        //        orderAscendingDirection = dtParameters.Order[0].Dir.ToString().ToLower() == "asc";
        //    }
        //    //var result = _context.TestRegisters.AsQueryable();
        //    var totalResultsCount = 0;
        //    DataTable tableq = new DataTable();
        //    using (DataSet result1 = await SqlHelper.ExecuteDatasetAsync(_configuration.GetConnectionString("DefaultConnection"), CommandType.StoredProcedure, "Usp_GetAll_EmployeeMaster"))
        //    {
        //        if (result1.Tables.Count > 0)
        //        {
        //            tableq = result1.Tables[0];
        //            if (tableq.Rows.Count > 0)
        //            {
        //                if (tableq.ContainColumn("TotalCount"))
        //                    totalResultsCount = Convert.ToInt32(tableq.Rows[0]["TotalCount"]);
        //                else
        //                    totalResultsCount = tableq.Rows.Count;
        //            }
        //        }
        //        //string json = JsonConvert.SerializeObject(tableq);
        //        //DataTable tbl = JsonConvert.DeserializeObject<DataTable>(json);
        //        PagedDataTable<EmployeeMaster> lst = tableq.ToPagedDataTableList<EmployeeMaster>
        //                (0, 10, 0, "", 1, 1);
        //        return lst;
        //        PagedDataTable<EmployeeMaster> rslt =
        //        return Json(new DtResult<EmployeeMaster>
        //        {
        //            Draw = dtParameters.Draw,
        //            RecordsTotal = totalResultsCount,
        //            RecordsFiltered = 10,
        //            Data = await tbl
        //                //.Skip(dtParameters.Start)
        //                //.Take(dtParameters.Length)
        //                .ToList<>
        //        });
        //    }
        //}

        #endregion Below code is for testing porpose for Jquery DataTables
    }
}

