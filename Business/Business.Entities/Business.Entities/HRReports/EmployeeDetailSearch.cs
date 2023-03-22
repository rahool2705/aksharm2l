using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Entities.HRReports
{
    public class EmployeeDetailSearch
    {
        public int EmployeeCategoryId { get; set; }

        public int DepartmentId { get; set; }

        public string SearchString { get; set; }
    }
}
