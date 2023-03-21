namespace Business.Entities.SalaryFormula
{
    public class SalaryFormula
    {
        public int SrNo { get; set; }
        public int SalaryFormulaID { get; set; }
        public int EmployeeCategoryId { get; set; }
        public string EmployeeCategoryText { get; set; }
        public int EmploymentTypeID { get; set; }
        public string EmploymentTypeText { get; set; }
        public int SalaryHeadID { get; set; }
        public string SalaryHeadName { get; set; }
        public string SalaryHeadLabel { get; set; }
        public string SalaryFormulaText { get; set; }
        public int SalaryTypeID { get; set; }
        public string SalaryTypeText { get; set; }
        public decimal LabelPercentage { get; set; }
        public string LabelValue { get; set; }
        public bool IsActive { get; set; }
        public int CreatedOrModifiedBy { get; set; }
    }
}
