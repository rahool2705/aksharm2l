namespace Business.Entities.SalaryFormula
{
    public class SalaryFormulaMaster
    {
        public int SalaryFormulaID { get; set; }
        public string X_CTC { get; set; }
        public string A_BasicOf { get; set; }
        public int Basic { get; set; }
        public string B_DearnessAllowenceOf { get; set; }
        public int DearnessAllowence { get; set; }
        public string DearnessAllowenceVl { get; set; }
        public string C_HouseRentAllowenceOf { get; set; }
        public int HouseRentAllowence { get; set; }
        public string HouseRentAllowenceVl { get; set; }
        public string D_OtherAllowenceOf { get; set; }
        public int OtherAllowence { get; set; }
        public string OtherAllowenceVl { get; set; }
        public string E_AllCashReembersmentOf { get; set; }
        public int AllCashReembersment { get; set; }
        public string AllCashReembersmentVl { get; set; }
        public string F_LeaveTravelAllowenceOf { get; set; }
        public int LeaveTravelAllowence { get; set; }
        public string LeaveTravelAllowenceVl { get; set; }
        public string G_MedicalOf { get; set; }
        public int Medical { get; set; }
        public string MedicalVl { get; set; }
        public string H_ArrearsOf { get; set; }
        public int Arrears { get; set; }
        public string ArrearsVl { get; set; }
        public string P_ProvidentFundOf { get; set; }
        public int ProvidentFund { get; set; }
        public string ProvidentFundVl { get; set; }
        public string Q_EmployeeStateInsuranceOf { get; set; }
        public int EmployeeStateInsurance { get; set; }
        public string EmployeeStateInsuranceVl { get; set; }
        public string R_IncomeTaxOf { get; set; }
        public int IncomeTax { get; set; }
        public string IncomeTaxVl { get; set; }
        public string S_ProfessionalTaxOf { get; set; }
        public int ProfessionalTax { get; set; }
        public string ProfessionalTaxVl { get; set; }
        public string T_LoanAndAdvanceOf { get; set; }
        public int LoanAndAdvance { get; set; }
        public string LoanAndAdvanceVl { get; set; }
        public string U_PrerequisitesOf { get; set; }
        public int Prerequisites { get; set; }
        public string PrerequisiteVl { get; set; }
        public string A1_GrossEarnings { get; set; }
        public string B1_GrossDeduction { get; set; }
        public string A1B1_NetSalaryPayable { get; set; }
    }
}
