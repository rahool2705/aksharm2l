namespace Business.Entities.Employee
{
    public class EmployeeSalaryBreakup
    {
        public int EmployeeID { get; set; }
        public int EmployeeSalaryBreakupID { get; set; }
        public string Basic { get; set; }
        public string DearnessAllowence { get; set; }
        public string HouseRentAllowence { get; set; }
        public string OtherAllowence { get; set; }
        public string AllCashReembersment { get; set; }
        public string LTA { get; set; }
        public string Medical { get; set; }
        public string Arrears { get; set; }
        public string ProvidentFund { get; set; }
        public string EmployeeStateInsurance { get; set; }
        public string IncomeTax { get; set; }
        public string ProfessionalTax { get; set; }
        public string LoanAndAdvance { get; set; }
        public string Prerequisites { get; set; }
        public string GrossEarnings { get; set; }
        public string GrossDeduction { get; set; }
        public string NetSalaryPayable { get; set; }
        public string CostToCompany { get; set; }
        public bool SalaryCalculateWithFormula { get; set; }
        public int CreatedOrModifiedBy { get; set; }
    }
}
