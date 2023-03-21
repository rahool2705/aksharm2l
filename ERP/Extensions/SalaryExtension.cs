using Business.Interface;
using Business.Interface.ISalaryFormula;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace ERP.Extensions
{
    public class SalaryExtension
    {
        private static HttpContext Current => new HttpContextAccessor().HttpContext;
        public static ISalaryFormula _salaryFormula => (ISalaryFormula)Current.RequestServices.GetService(typeof(ISalaryFormula));
        public static IMasterService _masterService => (IMasterService)Current.RequestServices.GetService(typeof(IMasterService));
        public static SelectList GetAllSalaryHead()
        {
            try
            {
                var employeeType = _masterService.GetAllSalaryHead();
                return new SelectList(employeeType, "SalaryHeadID", "SalaryHeadName");
            }
            catch
            {
                return new SelectList(Enumerable.Empty<SelectListItem>());
            }
        }

        public static SelectList GetAllSalaryType()
        {
            try
            {
                var employeeType = _masterService.GetAllSalaryType();
                return new SelectList(employeeType, "SalaryTypeID", "SalaryTypeText");
            }
            catch
            {
                return new SelectList(Enumerable.Empty<SelectListItem>());
            }
        }
    }
}
