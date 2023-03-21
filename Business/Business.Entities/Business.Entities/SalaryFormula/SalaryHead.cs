namespace Business.Entities.SalaryFormula
{
    public class SalaryHead
    {
        public int SrNo { get; set; }
        public int SalaryHeadID { get; set; }
        public string SalaryHeadName { get; set; }
        public bool IsActive { get; set; } = true;
        public int CreatedOrModifiedBy { get; set; }
    }
}
